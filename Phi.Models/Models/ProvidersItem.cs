using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class ProvidersItem
    {
        public int Id { get; set; }
        public Nullable<int> ItemProvidersId { get; set; }
        public Nullable<int> ItemId { get; set; }
        public virtual Item Item { get; set; }
        public virtual ItemProvider ItemProvider { get; set; }
    }
}
