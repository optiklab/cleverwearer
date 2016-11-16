using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class Item
    {
        public Item()
        {
            this.Images = new List<Image>();
            this.SuggestionItems = new List<SuggestionItem>();
            this.ProvidersItems = new List<ProvidersItem>();
            this.ItemsViaParameters = new List<ItemsViaParameter>();
            this.ItemLikes = new List<ItemLike>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MadeBy { get; set; }
        public string ProvideBy { get; set; }
        public Nullable<int> SuggestionTerms { get; set; }
        public Nullable<int> LanguageId { get; set; }
        public bool Gender { get; set; }
        public Nullable<int> Season { get; set; }
        public Nullable<int> WaterProtectionPercent { get; set; }
        public Nullable<bool> IceProtectionPercent { get; set; }
        public Nullable<int> ArmoringPercent { get; set; }
        public Nullable<int> MinAge { get; set; }
        public Nullable<int> MaxAge { get; set; }
        public Nullable<int> SunProtectionPercent { get; set; }
        public Nullable<int> ActionTypeId { get; set; }
        public Nullable<System.DateTime> Year { get; set; }
        public Nullable<int> ItemTypeId { get; set; }
        public Nullable<bool> IsPublic { get; set; }
        public Nullable<int> Root { get; set; }
        public string DefaultImageUri { get; set; }
        public bool IsChild { get; set; }
        public bool IsAvailable { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Referrer { get; set; }
        public Nullable<int> Currency { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.DateTime> AvailableTill { get; set; }
        public Nullable<int> Likes { get; set; }
        public bool IsWardrobe { get; set; }
        public Nullable<int> ShowedTimes { get; set; }
        public virtual ActionType ActionType { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<SuggestionItem> SuggestionItems { get; set; }
        public virtual ICollection<ProvidersItem> ProvidersItems { get; set; }
        public virtual ICollection<ItemsViaParameter> ItemsViaParameters { get; set; }
        public virtual Language Language { get; set; }
        public virtual ItemType ItemType { get; set; }
        public virtual ICollection<ItemLike> ItemLikes { get; set; }
    }
}
