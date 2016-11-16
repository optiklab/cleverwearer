using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class ItemsViaParameterMap : EntityTypeConfiguration<ItemsViaParameter>
    {
        public ItemsViaParameterMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("ItemsViaParameters");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ItemId).HasColumnName("ItemId");
            this.Property(t => t.ParameterId).HasColumnName("ParameterId");

            // Relationships
            this.HasOptional(t => t.Item)
                .WithMany(t => t.ItemsViaParameters)
                .HasForeignKey(d => d.ItemId);
            this.HasOptional(t => t.ItemParameter)
                .WithMany(t => t.ItemsViaParameters)
                .HasForeignKey(d => d.ParameterId);

        }
    }
}
