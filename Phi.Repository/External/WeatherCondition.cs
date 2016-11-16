using System;

namespace Phi.Repository.External
{
    public class WeatherCondition
    {
        /// <summary>
        /// 
        /// </summary>
        public WeatherCondition()
        {
        }

        /// <summary>
        /// Copy-constructor
        /// </summary>
        /// <param name="toCopy"></param>
        public WeatherCondition(WeatherCondition toCopy)
        {
            this.Temperature = toCopy.Temperature;
            this.TemperatureMin = toCopy.TemperatureMin;
            this.TemperatureMax = toCopy.TemperatureMax;
            this.FullDescription = toCopy.FullDescription;
            this.ShortDescription = toCopy.ShortDescription;
            this.WindSpeed = toCopy.WindSpeed;
            this.WindDirection = toCopy.WindDirection;
            this.AthmosphereHumidity = toCopy.AthmosphereHumidity;
            this.AthmospherePressure = toCopy.AthmospherePressure;
            this.AthmosphereVisibility = toCopy.AthmosphereVisibility;
            this.AthmosphereRising = toCopy.AthmosphereRising;
            this.Precipitation = toCopy.Precipitation;
            this.Condition = toCopy.Condition;
            this.GenereationDate = toCopy.GenereationDate;
            this.GenerationDateString = toCopy.GenerationDateString;
            this.IsForecast = toCopy.IsForecast;
            this.ForecastGuid = toCopy.ForecastGuid;
            this.ForecastDate = toCopy.ForecastDate;
            this.ForecastDateString = toCopy.ForecastDateString;
            this.Sunrise = toCopy.Sunrise;
            this.Sunset = toCopy.Sunset;
            this.GroundLevel = toCopy.GroundLevel;
            this.DistanceUnits = toCopy.DistanceUnits;
            this.PressureUnits = toCopy.PressureUnits;
            this.SpeedUnits = toCopy.SpeedUnits;
            this.TemperatureUnits = toCopy.TemperatureUnits;
            this.Language = toCopy.Language;
            this.City = toCopy.City;
            this.Region = toCopy.Region;
            this.Country = toCopy.Country;
            this.SeaLevel = toCopy.SeaLevel;
            this.IsPrecalculatedEffectiveTemperature = toCopy.IsPrecalculatedEffectiveTemperature;
            this.EffectiveTemperature = toCopy.EffectiveTemperature;
        }

        public Nullable<double> Temperature { get; set; }
        public Nullable<double> TemperatureMin { get; set; }
        public Nullable<double> TemperatureMax { get; set; }
        public string FullDescription { get; set; }
        public string ShortDescription { get; set; }
        public Nullable<double> WindSpeed { get; set; }
        public Nullable<double> WindDirection { get; set; }
        public Nullable<double> AthmosphereHumidity { get; set; }
        public Nullable<double> AthmospherePressure { get; set; }
        public Nullable<double> AthmosphereVisibility { get; set; }
        public Nullable<double> AthmosphereRising { get; set; }
        public Nullable<int> Condition { get; set; }
        public int Precipitation { get; set; }
        public Nullable<System.DateTime> GenereationDate { get; set; }
        public string GenerationDateString { get; set; }

        public bool IsForecast { get; set; }
        public string ForecastGuid { get; set; }
        public Nullable<System.DateTime> ForecastDate { get; set; }
        public string ForecastDateString { get; set; }

        public Nullable<System.DateTime> Sunrise { get; set; }
        public Nullable<System.DateTime> Sunset { get; set; }
        public Nullable<double> GroundLevel { get; set; }

        public string DistanceUnits { get; set; }
        public string PressureUnits { get; set; }
        public string SpeedUnits { get; set; }
        public string TemperatureUnits { get; set; }

        public string Language { get; set; }

        public string Woeid { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }


        public Nullable<double> SeaLevel { get; set; }
        public bool IsPrecalculatedEffectiveTemperature { get; set; }
        public Nullable<double> EffectiveTemperature { get; set; }
    }
}
