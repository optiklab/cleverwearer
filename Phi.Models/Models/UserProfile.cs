using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class UserProfile
    {
        public UserProfile()
        {
            this.ItemLikes = new List<ItemLike>();
            this.UserProfilesViaItemProviders = new List<UserProfilesViaItemProvider>();
        }

        public Nullable<bool> Gender { get; set; }
        public Nullable<int> LocationId { get; set; }
        public string PhiUserId { get; set; }
        public string AvatarPictureUrl { get; set; }
        public bool IsCompany { get; set; }
        public string MainCompanyUrl { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCEO { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyFax { get; set; }
        public string AdditionalInfo { get; set; }
        public string SellCompanyUrl { get; set; }
        public bool NotifyMeAboutSuddenWeatherEvents { get; set; }
        public virtual ICollection<ItemLike> ItemLikes { get; set; }
        public virtual Location Location { get; set; }
        public virtual PhiUser PhiUser { get; set; }
        public virtual ICollection<UserProfilesViaItemProvider> UserProfilesViaItemProviders { get; set; }
    }
}
