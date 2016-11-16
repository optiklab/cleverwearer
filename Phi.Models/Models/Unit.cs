using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class Unit
    {
        public Unit()
        {
            this.ItemParameters = new List<ItemParameter>();
            this.WeatherConditions = new List<WeatherCondition>();
        }

        public int Id { get; set; }
        public string SystemName { get; set; }
        public string Pressure { get; set; }
        public string Temperature { get; set; }
        public string Distance { get; set; }
        public string Speed { get; set; }
        public string Light { get; set; }
        public Nullable<int> LanguageId { get; set; }
        public string Humidity { get; set; }
        public virtual ICollection<ItemParameter> ItemParameters { get; set; }
        public virtual Language Language { get; set; }
        public virtual ICollection<WeatherCondition> WeatherConditions { get; set; }
    }
}
