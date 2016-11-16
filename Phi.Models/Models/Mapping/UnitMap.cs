using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class UnitMap : EntityTypeConfiguration<Unit>
    {
        public UnitMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.SystemName)
                .HasMaxLength(50);

            this.Property(t => t.Pressure)
                .HasMaxLength(50);

            this.Property(t => t.Temperature)
                .HasMaxLength(50);

            this.Property(t => t.Distance)
                .HasMaxLength(50);

            this.Property(t => t.Speed)
                .HasMaxLength(50);

            this.Property(t => t.Light)
                .HasMaxLength(50);

            this.Property(t => t.Humidity)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Units");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SystemName).HasColumnName("SystemName");
            this.Property(t => t.Pressure).HasColumnName("Pressure");
            this.Property(t => t.Temperature).HasColumnName("Temperature");
            this.Property(t => t.Distance).HasColumnName("Distance");
            this.Property(t => t.Speed).HasColumnName("Speed");
            this.Property(t => t.Light).HasColumnName("Light");
            this.Property(t => t.LanguageId).HasColumnName("LanguageId");
            this.Property(t => t.Humidity).HasColumnName("Humidity");

            // Relationships
            this.HasOptional(t => t.Language)
                .WithMany(t => t.Units)
                .HasForeignKey(d => d.LanguageId);

        }
    }
}
