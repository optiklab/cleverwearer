using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class ItemLike
    {
        public int Id { get; set; }
        public Nullable<int> ItemId { get; set; }
        public string PhiUserId { get; set; }
        public Nullable<bool> IsWish { get; set; }
        public Nullable<bool> IsLike { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public virtual Item Item { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
