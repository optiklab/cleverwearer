using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class SuggestionTerm
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Nullable<int> Value { get; set; }
        public Nullable<int> LanguageId { get; set; }
        public virtual Language Language { get; set; }
    }
}
