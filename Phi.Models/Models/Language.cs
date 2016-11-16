using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class Language
    {
        public Language()
        {
            this.ActionTypes = new List<ActionType>();
            this.ConditionDescriptions = new List<ConditionDescription>();
            this.GoodThoughts = new List<GoodThought>();
            this.Items = new List<Item>();
            this.ItemTypes = new List<ItemType>();
            this.WeatherConditions = new List<WeatherCondition>();
            this.Suggestions = new List<Suggestion>();
            this.Units = new List<Unit>();
            this.SuggestionTerms = new List<SuggestionTerm>();
            this.SeasonTypes = new List<SeasonType>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Fullname { get; set; }
        public virtual ICollection<ActionType> ActionTypes { get; set; }
        public virtual ICollection<ConditionDescription> ConditionDescriptions { get; set; }
        public virtual ICollection<GoodThought> GoodThoughts { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<ItemType> ItemTypes { get; set; }
        public virtual ICollection<WeatherCondition> WeatherConditions { get; set; }
        public virtual ICollection<Suggestion> Suggestions { get; set; }
        public virtual ICollection<Unit> Units { get; set; }
        public virtual ICollection<SuggestionTerm> SuggestionTerms { get; set; }
        public virtual ICollection<SeasonType> SeasonTypes { get; set; }
    }
}
