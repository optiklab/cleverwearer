using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class SeasonViaLocationMap : EntityTypeConfiguration<SeasonViaLocation>
    {
        public SeasonViaLocationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("SeasonViaLocation");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SeasonId).HasColumnName("SeasonId");
            this.Property(t => t.LocationId).HasColumnName("LocationId");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");

            // Relationships
            this.HasOptional(t => t.Location)
                .WithMany(t => t.SeasonViaLocations)
                .HasForeignKey(d => d.LocationId);
            this.HasOptional(t => t.SeasonType)
                .WithMany(t => t.SeasonViaLocations)
                .HasForeignKey(d => d.SeasonId);

        }
    }
}
