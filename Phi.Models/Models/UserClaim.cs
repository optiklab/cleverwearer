using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class UserClaim
    {
        public int Id { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public string PhiUserId { get; set; }
        public virtual PhiUser PhiUser { get; set; }
    }
}
