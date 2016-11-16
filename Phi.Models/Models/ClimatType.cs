using System;
using System.Collections.Generic;

namespace Phi.Models.Models
{
    public partial class ClimatType
    {
        public ClimatType()
        {
            this.Locations = new List<Location>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Belt { get; set; }
        public string AlternativeNames { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
    }
}
