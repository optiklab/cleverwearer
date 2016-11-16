using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class UserAttribute
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public int Id { get; set; }
        public string PhiUserId { get; set; }
        public virtual PhiUser PhiUser { get; set; }
    }
}
