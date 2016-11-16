using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class UserRole
    {
        public System.DateTime AddRoleDate { get; set; }
        public string PhiUserId { get; set; }
        public string RoleId { get; set; }
        public virtual PhiUser PhiUser { get; set; }
        public virtual Role Role { get; set; }
    }
}
