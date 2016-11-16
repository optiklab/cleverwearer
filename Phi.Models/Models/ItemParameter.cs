using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class ItemParameter
    {
        public ItemParameter()
        {
            this.ItemsViaParameters = new List<ItemsViaParameter>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Nullable<int> UnitId { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<ItemsViaParameter> ItemsViaParameters { get; set; }
    }
}
