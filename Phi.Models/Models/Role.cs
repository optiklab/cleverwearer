using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace Phi.Models.Models
{
    public partial class Role : IRole
    {
        public Role()
        {
            this.Id = Guid.NewGuid().ToString();
            this.UserRoles = new List<UserRole>();
        }

        public string Name { get; set; }
        public bool Active { get; set; }
        public string Id { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
