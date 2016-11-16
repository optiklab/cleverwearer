using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class FactorType
    {
        public FactorType()
        {
            this.Factors = new List<Factor>();
            this.Rules = new List<Rule>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Factor> Factors { get; set; }
        public virtual ICollection<Rule> Rules { get; set; }
    }
}
