using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class SuggestionTermMap : EntityTypeConfiguration<SuggestionTerm>
    {
        public SuggestionTermMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Code)
                .HasMaxLength(255);

            this.Property(t => t.Name)
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("SuggestionTerm");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.LanguageId).HasColumnName("LanguageId");

            // Relationships
            this.HasOptional(t => t.Language)
                .WithMany(t => t.SuggestionTerms)
                .HasForeignKey(d => d.LanguageId);

        }
    }
}
