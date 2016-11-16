using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class AlertMap : EntityTypeConfiguration<Alert>
    {
        public AlertMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Alerts");
            this.Property(t => t.Id).HasColumnName("Id");
        }
    }
}
