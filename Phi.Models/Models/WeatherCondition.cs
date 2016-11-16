using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class WeatherCondition
    {
        public WeatherCondition()
        {
            this.Suggestions = new List<Suggestion>();
        }

        public int Id { get; set; }
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
        public int Precipitation { get; set; }
        public Nullable<System.DateTime> GenereationDate { get; set; }
        public bool IsForecast { get; set; }
        public string ForecastGuid { get; set; }
        public Nullable<System.DateTime> ForecastDate { get; set; }
        public Nullable<System.DateTime> Sunrise { get; set; }
        public Nullable<System.DateTime> Sunset { get; set; }
        public Nullable<double> GroundLevel { get; set; }
        public Nullable<int> UnitsId { get; set; }
        public Nullable<int> LanguageId { get; set; }
        public Nullable<double> SeaLevel { get; set; }
        public bool IsPrecalculatedEffectiveTemperature { get; set; }
        public Nullable<double> EffectiveTemperature { get; set; }
        public Nullable<int> LocationId { get; set; }
        public string GenerationDateString { get; set; }
        public string ForecastDateString { get; set; }
        public Nullable<int> DataProviderId { get; set; }
        public Nullable<int> Condition { get; set; }
        public virtual DataProvider DataProvider { get; set; }
        public virtual Language Language { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<Suggestion> Suggestions { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
