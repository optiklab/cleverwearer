using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class GoodThoughtMap : EntityTypeConfiguration<GoodThought>
    {
        public GoodThoughtMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(4000);

            this.Property(t => t.Author)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("GoodThoughts");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Author).HasColumnName("Author");
            this.Property(t => t.LanguageId).HasColumnName("LanguageId");

            // Relationships
            this.HasOptional(t => t.Language)
                .WithMany(t => t.GoodThoughts)
                .HasForeignKey(d => d.LanguageId);

        }
    }
}
