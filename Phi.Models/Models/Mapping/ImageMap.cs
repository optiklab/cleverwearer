using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class ImageMap : EntityTypeConfiguration<Image>
    {
        public ImageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ImageUrl)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Images");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ImageUrl).HasColumnName("ImageUrl");
            this.Property(t => t.Height).HasColumnName("Height");
            this.Property(t => t.Width).HasColumnName("Width");
            this.Property(t => t.ItemId).HasColumnName("ItemId");

            // Relationships
            this.HasOptional(t => t.Item)
                .WithMany(t => t.Images)
                .HasForeignKey(d => d.ItemId);

        }
    }
}
