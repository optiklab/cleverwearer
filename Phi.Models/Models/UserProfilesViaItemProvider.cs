using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class UserProfilesViaItemProvider
    {
        public string PhiUserId { get; set; }
        public int Id { get; set; }
        public Nullable<int> ItemProviderId { get; set; }
        public virtual ItemProvider ItemProvider { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
