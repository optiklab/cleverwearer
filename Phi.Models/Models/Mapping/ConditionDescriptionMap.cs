using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class ConditionDescriptionMap : EntityTypeConfiguration<ConditionDescription>
    {
        public ConditionDescriptionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ShortDescription)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(255);

            this.Property(t => t.Icon)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ConditionDescription");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ExtId).HasColumnName("ExtId");
            this.Property(t => t.ShortDescription).HasColumnName("ShortDescription");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.LanguageId).HasColumnName("LanguageId");
            this.Property(t => t.Icon).HasColumnName("Icon");

            // Relationships
            this.HasOptional(t => t.Language)
                .WithMany(t => t.ConditionDescriptions)
                .HasForeignKey(d => d.LanguageId);

        }
    }
}
