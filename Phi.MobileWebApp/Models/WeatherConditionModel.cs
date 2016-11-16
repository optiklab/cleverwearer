/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.MobileWebApp.Models
{
    using System;
using System.Collections.Generic;

    public class WeatherConditionModel
    {
        #region Private fields

        private const string ZERO = "0";

        #endregion

        #region Constructor

        public WeatherConditionModel()
        {
            ServerDate = DateTime.UtcNow.Date;
            AthmosphereHumidity = ZERO;
            AthmospherePressure = ZERO;
            AthmosphereVisibility = ZERO;
            AthmosphereRising = ZERO;
            Temperature = ZERO;
            TemperatureMax = ZERO;
            TemperatureMin = ZERO;
            EffectiveTemperature = ZERO;
            SeaLevel = ZERO;
            GroundLevel = ZERO;

            Forecasts = new Dictionary<string, DateTime>();
        }

        #endregion

        #region Condition fields

        public DateTime ServerDate { get; set; }
        public string Temperature { get; set; }
        public string TemperatureMin { get; set; }
        public string TemperatureMax { get; set; }
        public string FullDescription { get; set; }
        public string ShortDescription { get; set; }
        public string WindSpeed { get; set; }
        public string WindDirection { get; set; }
        public bool IsWindy { get; set; }
        public string AthmosphereHumidity { get; set; }
        public string AthmospherePressure { get; set; }
        public string AthmosphereVisibility { get; set; }
        public string AthmosphereRising { get; set; }
        public int Precipitation { get; set; }
        public Boolean IsForecast { get; set; }
        public string ForecastGuid { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
        public string GroundLevel { get; set; }
        public string SeaLevel { get; set; }
        public string EffectiveTemperature { get; set; }
        public string GenerationDateString { get; set; }
        public string ForecastDateString { get; set; }
        public DateTime ForecastDate { get; set; }
        public string ConditionIcon { get; set; }

        public string DataProviderName { get; set; }
        public string Language { get; set; }

        public string Location { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public string SpeedUnits { get; set; }
        public string TemperatureUnits { get; set; }
        public string HumidityUnits { get; set; }
        public string PressureUnits { get; set; }
        public string DistanceUnits { get; set; }
        public string LightUnits { get; set; }
        public Dictionary<string, DateTime> Forecasts { get; set; }

        #endregion
    }
}