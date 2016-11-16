using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class ItemType
    {
        public ItemType()
        {
            this.Items = new List<Item>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> LanguageId { get; set; }
        public Nullable<int> ItemProviderId { get; set; }
        public Nullable<int> EnumType { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ItemProvider ItemProvider { get; set; }
        public virtual Language Language { get; set; }
    }
}
