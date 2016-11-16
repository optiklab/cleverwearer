using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class ItemsViaParameter
    {
        public int Id { get; set; }
        public Nullable<int> ItemId { get; set; }
        public Nullable<int> ParameterId { get; set; }
        public virtual Item Item { get; set; }
        public virtual ItemParameter ItemParameter { get; set; }
    }
}
