using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class ConditionDescription
    {
        public int Id { get; set; }
        public int ExtId { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public Nullable<int> LanguageId { get; set; }
        public string Icon { get; set; }
        public virtual Language Language { get; set; }
    }
}
