using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class SuggestionItemMap : EntityTypeConfiguration<SuggestionItem>
    {
        public SuggestionItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("SuggestionItems");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SuggestionId).HasColumnName("SuggestionId");
            this.Property(t => t.ItemId).HasColumnName("ItemId");

            // Relationships
            this.HasOptional(t => t.Item)
                .WithMany(t => t.SuggestionItems)
                .HasForeignKey(d => d.ItemId);
            this.HasOptional(t => t.Suggestion)
                .WithMany(t => t.SuggestionItems)
                .HasForeignKey(d => d.SuggestionId);

        }
    }
}
