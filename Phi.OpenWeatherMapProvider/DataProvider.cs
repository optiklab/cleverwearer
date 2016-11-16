using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Phi.Repository;
using Phi.OpenWeatherMapProvider.WeatherModels;
using Phi.Models.Models;
using Phi.Repository.Enums;

namespace Phi.OpenWeatherMapProvider
{
    /// <summary>
    /// 
    /// </summary>
    public class DataProvider : IDataProvider
    {
        #region Public methods

        public string ProviderName { get { return "openweathermap"; } }

        public string GetUnitsName(Units units)
        {
            switch(units)
            {
                case Units.Imperial:
                    return "imperial";
                case Units.Metric:
                    return "metric";
                default:
                    Debug.Assert(false);
                    return "metric";
            }
        }

        /// <summary>
        /// Gets the weather forecast by woeid.
        /// </summary>
        /// <param name="woeid">The woeid.</param>
        /// <param name="system">Metric or USA system.</param>
        /// <returns></returns>
        public IEnumerable<Phi.Repository.External.WeatherCondition> GetWeatherForecastByWoeid(string woeid, string system)
        {
            var forecasts = new List<Repository.External.WeatherCondition>();

            try
            {
                // Only current weather.
                //forecasts.Add(GetWeatherByWoeid(woeid, system));

                // Forecast.
                forecasts.AddRange(GetWeatherForecast(woeid, system));
            }
            catch (Exception)
            {
            }

            return forecasts;
        }

        public Location GetCityLocation(string cityName)
        {
            throw new NotImplementedException();
        }

        public List<Location> GetCityLocations(string cityName)
        {
            throw new NotImplementedException();
        }

        public Location GetCityLocationByWoeid(string woeid)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Privates

        private Phi.Repository.External.WeatherCondition GetWeatherByWoeid(string woeid, string system)
        {
            var condition = new Phi.Repository.External.WeatherCondition();
            
            ServiceClient client = new ServiceClient();
            string wResponse = client.GetResponse(new XmlWeatherRequest(woeid, system).GetUrl());
            var result = XmlHelpers.ConvertStringToXMLObject(typeof(XmlCurrent), wResponse) as XmlCurrent;

            if (result != null)
            {
                // Convert to weather objects
                condition.Woeid = woeid;

                if (result.City != null)
                {
                    condition.City = result.City.Name;
                    condition.Country = result.City.Country;

                    if (result.City.Sun != null)
                    {
                        DateTime sunrise;
                        if (DateTime.TryParse(result.City.Sun.Rise, out sunrise))
                        {
                            condition.Sunrise = sunrise;
                        }

                        DateTime sunset;
                        if (DateTime.TryParse(result.City.Sun.Set, out sunset))
                        {
                            condition.Sunset = sunset;
                        }
                    }

                    if (result.City.Coord != null)
                    {
                        condition.Latitude = result.City.Coord.Latitude;
                        condition.Longitude = result.City.Coord.Longitude;
                    }
                }

                condition.DistanceUnits = system == "metric" ? "m" : "in";
                condition.SpeedUnits = system == "metric" ? "m/s" : "in/s";
                condition.TemperatureUnits = system == "metric" ? "C" : "F";
                condition.Language = "English";

                if (result.Wind != null)
                {
                    double windDirection = 0;
                    double windSpeed = 0;

                    if (Double.TryParse(result.Wind.Direction.Value, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out windDirection))
                    {
                        condition.WindDirection = windDirection;
                    }

                    if (Double.TryParse(result.Wind.Speed.Value, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out windSpeed))
                    {
                        condition.WindSpeed = windSpeed;
                    }
                }

                if (result.Humidity != null)
                {
                    double humidity = 0;
                    if (Double.TryParse(result.Humidity.Value, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out humidity))
                    {
                        condition.AthmosphereHumidity = humidity;
                    }

                }

                if (result.Pressure != null)
                {
                    condition.PressureUnits = result.Pressure.Unit;
                    double pressure = 0;
                    if (Double.TryParse(result.Pressure.Value, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out pressure))
                    {
                        condition.AthmospherePressure = pressure;
                    }
                }

                if (result.Visibility != null)
                {
                    double visibility = 0;
                    if (Double.TryParse(result.Visibility.Value, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out visibility))
                    {
                        condition.AthmosphereVisibility = visibility;
                    }
                }

                if (result.LastUpdate != null)
                {
                    condition.GenerationDateString = result.LastUpdate.Value;
                    condition.ForecastDateString = result.LastUpdate.Value;

                    DateTime genereationDate;
                    if (DateTime.TryParse(condition.GenerationDateString, out genereationDate))
                    {
                        condition.GenereationDate = genereationDate;
                        condition.ForecastDate = genereationDate;
                    }
                    else
                    {
                        condition.GenereationDate = DateTime.UtcNow;
                        condition.ForecastDate = DateTime.UtcNow;
                    }
                }
                else
                {
                    condition.GenerationDateString = DateTime.UtcNow.ToString();
                    condition.ForecastDateString = DateTime.UtcNow.ToString();
                }

                condition.FullDescription = result.Weather == null ? string.Empty : result.Weather.Value;
                condition.ShortDescription = result.Weather == null ? string.Empty : result.Weather.Value;
                condition.Precipitation = result.Precipitation == null ? 0 : ConvertPrecipitation(result.Precipitation.Mode);
                condition.IsForecast = false;
                condition.SeaLevel = 0;
                condition.GroundLevel = 0;

                condition.IsPrecalculatedEffectiveTemperature = true;

                if (result.Weather != null)
                {
                    int conditionCode = -1;
                    if (Int32.TryParse(result.Weather.Number, out conditionCode))
                    {
                        condition.Condition = conditionCode;
                    }
                }

                if (result.Temperature != null)
                {
                    double minTemperature = 0;
                    if (Double.TryParse(result.Temperature.Min, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out minTemperature))
                    {
                        condition.TemperatureMin = minTemperature;
                    }

                    double maxTemperature = 0;
                    if (Double.TryParse(result.Temperature.Max, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out maxTemperature))
                    {
                        condition.TemperatureMax = maxTemperature;
                    }

                    double effectiveTemperature = 0;
                    if (Double.TryParse(result.Temperature.Value, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out effectiveTemperature))
                    {
                        condition.EffectiveTemperature = effectiveTemperature;
                    }
                }
            }

            return condition;
        }

        private List<Phi.Repository.External.WeatherCondition> GetWeatherForecast(string woeid, string system)
        {
            var forecasts = new List<Repository.External.WeatherCondition>();

            ServiceClient client = new ServiceClient();
            string fResponse = client.GetResponse(new XmlWeatherForecastRequest(woeid, system).GetUrl());
            var result = XmlHelpers.ConvertStringToXMLObject(typeof(XmlWeatherData), fResponse) as XmlWeatherData;

            if (result != null)
            {
                DateTime lastDay = DateTime.UtcNow;
                bool current = true;
                foreach (var time in result.Forecast.Times)
                {
                    var condition = new Phi.Repository.External.WeatherCondition();
                    condition.IsForecast = true;

                    DateTime forecastDate;
                    if (DateTime.TryParse(time.From, out forecastDate))
                    {
                        condition.GenereationDate = forecastDate;
                        condition.ForecastDate = forecastDate;
                    }
                    else
                    {
                        continue;
                    }

                    // Get only Morning-Day-Evening forecast, not night.
                    if (forecastDate.Hour < 6 || forecastDate.Hour > 21)
                    {
                        continue;
                    }

                    if (current)
                    {
                        lastDay = forecastDate.Date; // Set only first time.
                        condition.IsForecast = false;

                        if (result.Sun != null)
                        {
                            DateTime sunrise;
                            if (DateTime.TryParse(result.Sun.Rise, out sunrise))
                            {
                                condition.Sunrise = sunrise;
                            }

                            DateTime sunset;
                            if (DateTime.TryParse(result.Sun.Set, out sunset))
                            {
                                condition.Sunset = sunset;
                            }

                        }

                        current = false;
                    }
                    else
                    {
                        // Filter out same date and get forecast just for next date.
                        if (lastDay == forecastDate.Date)
                        {
                            continue;
                        }
                        else
                        {
                            lastDay = forecastDate.Date;
                        }
                    }

                    condition.ForecastDateString = forecastDate.ToString("ddd", CultureInfo.InvariantCulture);
                    condition.GenerationDateString = forecastDate.ToString("R", CultureInfo.InvariantCulture);
                    condition.Woeid = woeid;
                    condition.DistanceUnits = system == "metric" ? "m" : "in";
                    condition.SpeedUnits = system == "metric" ? "m/s" : "in/s";
                    condition.TemperatureUnits = system == "metric" ? "C" : "F";
                    condition.Language = "English";

                    condition.FullDescription = time.Symbol == null ? string.Empty : time.Symbol.Name;
                    condition.ShortDescription = time.Symbol == null ? string.Empty : time.Symbol.Name;
                    condition.Precipitation = time.Precipitation == null ? 0 : ConvertPrecipitation(time.Precipitation.Mode);
                    condition.IsForecast = false;
                    condition.SeaLevel = 0;
                    condition.GroundLevel = 0;

                    if (result.Location != null)
                    {
                        condition.City = result.Location.CityName;
                        condition.Country = result.Location.Country;
                    }

                    condition.IsPrecalculatedEffectiveTemperature = true;

                    if (time.Direction != null)
                    {
                        double windDirection = 0;
                        if (Double.TryParse(time.Direction.Value, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out windDirection))
                        {
                            condition.WindDirection = windDirection;
                        }
                    }
                    
                    if (time.Speed != null)
                    {
                        double windSpeed = 0;

                        if (Double.TryParse(time.Speed.Value, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out windSpeed))
                        {
                            condition.WindSpeed = windSpeed;
                        }
                    }

                    if (time.Humidity != null)
                    {
                        double humidity = 0;
                        if (Double.TryParse(time.Humidity.Value, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out humidity))
                        {
                            condition.AthmosphereHumidity = humidity;
                        }

                    }

                    if (time.Pressure != null)
                    {
                        condition.PressureUnits = time.Pressure.Unit;
                        double pressure = 0;
                        if (Double.TryParse(time.Pressure.Value, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out pressure))
                        {
                            condition.AthmospherePressure = pressure;
                        }
                    }

                    if (time.Symbol != null)
                    {
                        int conditionCode = -1;
                        if (Int32.TryParse(time.Symbol.Number, out conditionCode))
                        {
                            condition.Condition = conditionCode;
                        }
                    }

                    if (time.Temperature != null)
                    {
                        double minTemperature = 0;
                        if (Double.TryParse(time.Temperature.Min, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out minTemperature))
                        {
                            condition.TemperatureMin = minTemperature;
                        }

                        double maxTemperature = 0;
                        if (Double.TryParse(time.Temperature.Max, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out maxTemperature))
                        {
                            condition.TemperatureMax = maxTemperature;
                        }

                        double effectiveTemperature = 0;
                        if (Double.TryParse(time.Temperature.Value, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out effectiveTemperature))
                        {
                            condition.EffectiveTemperature = effectiveTemperature;
                        }
                    }

                    forecasts.Add(condition);
                }
            }

            return forecasts;
        }


        private int ConvertPrecipitation(string value)
        {
            // TODO Sometimes later.
            return 0;
        }

        #endregion
    }
}
