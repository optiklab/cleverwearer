using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class UserLogin
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string PhiUserId { get; set; }
        public virtual PhiUser PhiUser { get; set; }
    }
}
