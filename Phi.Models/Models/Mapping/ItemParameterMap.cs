using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class ItemParameterMap : EntityTypeConfiguration<ItemParameter>
    {
        public ItemParameterMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Code)
                .HasMaxLength(255);

            this.Property(t => t.Name)
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("ItemParameters");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.UnitId).HasColumnName("UnitId");

            // Relationships
            this.HasOptional(t => t.Unit)
                .WithMany(t => t.ItemParameters)
                .HasForeignKey(d => d.UnitId);

        }
    }
}
