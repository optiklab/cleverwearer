using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class LocationMap : EntityTypeConfiguration<Location>
    {
        public LocationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.WOEID)
                .HasMaxLength(255);

            this.Property(t => t.Continent)
                .HasMaxLength(255);

            this.Property(t => t.Country)
                .HasMaxLength(255);

            this.Property(t => t.Admin)
                .HasMaxLength(255);

            this.Property(t => t.Admin2)
                .HasMaxLength(255);

            this.Property(t => t.Admin3)
                .HasMaxLength(255);

            this.Property(t => t.Town)
                .HasMaxLength(255);

            this.Property(t => t.Suburb)
                .HasMaxLength(255);

            this.Property(t => t.Postal_Code)
                .HasMaxLength(20);

            this.Property(t => t.Supername)
                .HasMaxLength(255);

            this.Property(t => t.Colloquial)
                .HasMaxLength(255);

            this.Property(t => t.ShortName)
                .HasMaxLength(50);

            this.Property(t => t.FlagFileName)
                .HasMaxLength(255);

            this.Property(t => t.Parent_WOEID)
                .HasMaxLength(255);

            this.Property(t => t.ProviderTimeZone)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Location");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.WOEID).HasColumnName("WOEID");
            this.Property(t => t.Continent).HasColumnName("Continent");
            this.Property(t => t.Country).HasColumnName("Country");
            this.Property(t => t.Admin).HasColumnName("Admin");
            this.Property(t => t.Admin2).HasColumnName("Admin2");
            this.Property(t => t.Admin3).HasColumnName("Admin3");
            this.Property(t => t.Town).HasColumnName("Town");
            this.Property(t => t.Suburb).HasColumnName("Suburb");
            this.Property(t => t.Postal_Code).HasColumnName("Postal_Code");
            this.Property(t => t.Supername).HasColumnName("Supername");
            this.Property(t => t.Colloquial).HasColumnName("Colloquial");
            this.Property(t => t.Time_Zone).HasColumnName("Time_Zone");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.ClimatId).HasColumnName("ClimatId");
            this.Property(t => t.ShortName).HasColumnName("ShortName");
            this.Property(t => t.FlagFileName).HasColumnName("FlagFileName");
            this.Property(t => t.SWLatitude).HasColumnName("SWLatitude");
            this.Property(t => t.SWLongitude).HasColumnName("SWLongitude");
            this.Property(t => t.NELatitude).HasColumnName("NELatitude");
            this.Property(t => t.NELongitude).HasColumnName("NELongitude");
            this.Property(t => t.Parent_WOEID).HasColumnName("Parent_WOEID");
            this.Property(t => t.ProviderTimeZone).HasColumnName("ProviderTimeZone");

            // Relationships
            this.HasOptional(t => t.ClimatType)
                .WithMany(t => t.Locations)
                .HasForeignKey(d => d.ClimatId);

        }
    }
}
