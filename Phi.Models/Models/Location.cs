using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class Location
    {
        public Location()
        {
            this.ItemProviders = new List<ItemProvider>();
            this.SeasonViaLocations = new List<SeasonViaLocation>();
            this.WeatherConditions = new List<WeatherCondition>();
            this.UserProfiles = new List<UserProfile>();
        }

        public int Id { get; set; }
        public string WOEID { get; set; }
        public string Continent { get; set; }
        public string Country { get; set; }
        public string Admin { get; set; }
        public string Admin2 { get; set; }
        public string Admin3 { get; set; }
        public string Town { get; set; }
        public string Suburb { get; set; }
        public string Postal_Code { get; set; }
        public string Supername { get; set; }
        public string Colloquial { get; set; }
        public Nullable<int> Time_Zone { get; set; }
        public Nullable<double> Longitude { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<int> ClimatId { get; set; }
        public string ShortName { get; set; }
        public string FlagFileName { get; set; }
        public Nullable<double> SWLatitude { get; set; }
        public Nullable<double> SWLongitude { get; set; }
        public Nullable<double> NELatitude { get; set; }
        public Nullable<double> NELongitude { get; set; }
        public string Parent_WOEID { get; set; }
        public string ProviderTimeZone { get; set; }
        public virtual ClimatType ClimatType { get; set; }
        public virtual ICollection<ItemProvider> ItemProviders { get; set; }
        public virtual ICollection<SeasonViaLocation> SeasonViaLocations { get; set; }
        public virtual ICollection<WeatherCondition> WeatherConditions { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
