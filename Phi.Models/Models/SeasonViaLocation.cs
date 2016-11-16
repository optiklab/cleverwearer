using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class SeasonViaLocation
    {
        public int Id { get; set; }
        public Nullable<int> SeasonId { get; set; }
        public Nullable<int> LocationId { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public virtual Location Location { get; set; }
        public virtual SeasonType SeasonType { get; set; }
    }
}
