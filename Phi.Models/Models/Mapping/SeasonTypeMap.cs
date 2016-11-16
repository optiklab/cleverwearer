using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class SeasonTypeMap : EntityTypeConfiguration<SeasonType>
    {
        public SeasonTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("SeasonType");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.LanguageId).HasColumnName("LanguageId");

            // Relationships
            this.HasOptional(t => t.Language)
                .WithMany(t => t.SeasonTypes)
                .HasForeignKey(d => d.LanguageId);

        }
    }
}
