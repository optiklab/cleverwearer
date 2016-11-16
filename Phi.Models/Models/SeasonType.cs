using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class SeasonType
    {
        public SeasonType()
        {
            this.SeasonViaLocations = new List<SeasonViaLocation>();
        }

        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public Nullable<int> LanguageId { get; set; }
        public virtual Language Language { get; set; }
        public virtual ICollection<SeasonViaLocation> SeasonViaLocations { get; set; }
    }
}
