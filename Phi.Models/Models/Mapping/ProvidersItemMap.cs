using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class ProvidersItemMap : EntityTypeConfiguration<ProvidersItem>
    {
        public ProvidersItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProvidersItems");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ItemProvidersId).HasColumnName("ItemProvidersId");
            this.Property(t => t.ItemId).HasColumnName("ItemId");

            // Relationships
            this.HasOptional(t => t.Item)
                .WithMany(t => t.ProvidersItems)
                .HasForeignKey(d => d.ItemId);
            this.HasOptional(t => t.ItemProvider)
                .WithMany(t => t.ProvidersItems)
                .HasForeignKey(d => d.ItemProvidersId);

        }
    }
}
