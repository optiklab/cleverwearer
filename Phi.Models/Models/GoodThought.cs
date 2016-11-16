using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class GoodThought
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public Nullable<int> LanguageId { get; set; }
        public virtual Language Language { get; set; }
    }
}
