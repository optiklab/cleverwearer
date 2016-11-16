using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class Rule
    {
        public int Id { get; set; }
        public Nullable<double> MinValue { get; set; }
        public Nullable<double> MaxValue { get; set; }
        public Nullable<double> Value { get; set; }
        public Nullable<int> FactorTypeId { get; set; }
        public Nullable<int> Result { get; set; }
        public virtual FactorType FactorType { get; set; }
    }
}
