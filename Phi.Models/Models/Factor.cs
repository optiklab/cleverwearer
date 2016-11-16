using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class Factor
    {
        public int Id { get; set; }
        public Nullable<double> Value { get; set; }
        public Nullable<int> FactorTypeId { get; set; }
        public Nullable<int> ActionTypeId { get; set; }
        public virtual ActionType ActionType { get; set; }
        public virtual FactorType FactorType { get; set; }
    }
}
