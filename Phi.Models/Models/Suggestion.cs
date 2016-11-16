using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class Suggestion
    {
        public Suggestion()
        {
            this.SuggestionItems = new List<SuggestionItem>();
        }

        public int Id { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public Nullable<int> WeatherConditionId { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<int> LanguageId { get; set; }
        public virtual Language Language { get; set; }
        public virtual ICollection<SuggestionItem> SuggestionItems { get; set; }
        public virtual WeatherCondition WeatherCondition { get; set; }
    }
}
