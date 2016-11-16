using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class Image
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public Nullable<int> Height { get; set; }
        public Nullable<int> Width { get; set; }
        public Nullable<int> ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
