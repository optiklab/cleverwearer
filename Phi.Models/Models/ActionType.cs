using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class ActionType
    {
        public ActionType()
        {
            this.Items = new List<Item>();
            this.Factors = new List<Factor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> Code { get; set; }
        public string Description { get; set; }
        public Nullable<int> LanguageId { get; set; }
        public int ShowOrder { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Factor> Factors { get; set; }
        public virtual Language Language { get; set; }
    }
}
