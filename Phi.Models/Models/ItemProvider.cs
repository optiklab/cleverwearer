using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class ItemProvider
    {
        public ItemProvider()
        {
            this.ProvidersItems = new List<ProvidersItem>();
            this.UserProfilesViaItemProviders = new List<UserProfilesViaItemProvider>();
            this.ItemTypes = new List<ItemType>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PhisicalAddress { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Nullable<int> LocationId { get; set; }
        public bool IsPublic { get; set; }
        public Nullable<int> EnumType { get; set; }
        public virtual ICollection<ProvidersItem> ProvidersItems { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<UserProfilesViaItemProvider> UserProfilesViaItemProviders { get; set; }
        public virtual ICollection<ItemType> ItemTypes { get; set; }
    }
}
