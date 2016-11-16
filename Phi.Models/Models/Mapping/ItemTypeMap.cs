using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class ItemTypeMap : EntityTypeConfiguration<ItemType>
    {
        public ItemTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ItemType");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.LanguageId).HasColumnName("LanguageId");
            this.Property(t => t.ItemProviderId).HasColumnName("ItemProviderId");
            this.Property(t => t.EnumType).HasColumnName("EnumType");

            // Relationships
            this.HasOptional(t => t.ItemProvider)
                .WithMany(t => t.ItemTypes)
                .HasForeignKey(d => d.ItemProviderId);
            this.HasOptional(t => t.Language)
                .WithMany(t => t.ItemTypes)
                .HasForeignKey(d => d.LanguageId);

        }
    }
}
