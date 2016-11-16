using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class DataProvider
    {
        public DataProvider()
        {
            this.WeatherConditions = new List<WeatherCondition>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> ConnectionType { get; set; }
        public string Connection { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<WeatherCondition> WeatherConditions { get; set; }
    }
}
