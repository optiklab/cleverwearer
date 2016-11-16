using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class ItemLikeMap : EntityTypeConfiguration<ItemLike>
    {
        public ItemLikeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.PhiUserId)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("ItemLikes");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ItemId).HasColumnName("ItemId");
            this.Property(t => t.PhiUserId).HasColumnName("PhiUserId");
            this.Property(t => t.IsWish).HasColumnName("IsWish");
            this.Property(t => t.IsLike).HasColumnName("IsLike");
            this.Property(t => t.Created).HasColumnName("Created");

            // Relationships
            this.HasOptional(t => t.Item)
                .WithMany(t => t.ItemLikes)
                .HasForeignKey(d => d.ItemId);
            this.HasOptional(t => t.UserProfile)
                .WithMany(t => t.ItemLikes)
                .HasForeignKey(d => d.PhiUserId);

        }
    }
}
