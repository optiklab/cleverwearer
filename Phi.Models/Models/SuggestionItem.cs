using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class SuggestionItem
    {
        public int Id { get; set; }
        public Nullable<int> SuggestionId { get; set; }
        public Nullable<int> ItemId { get; set; }
        public virtual Item Item { get; set; }
        public virtual Suggestion Suggestion { get; set; }
    }
}
