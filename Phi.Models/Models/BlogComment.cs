using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class BlogComment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public Nullable<int> BlogId { get; set; }
        public virtual Blog Blog { get; set; }
        public virtual PhiUser PhiUser { get; set; }
    }
}
