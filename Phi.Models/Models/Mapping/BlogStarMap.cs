using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class BlogStarMap : EntityTypeConfiguration<BlogStar>
    {
        public BlogStarMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.UserId)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("BlogStars");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.BlogId).HasColumnName("BlogId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Stars).HasColumnName("Stars");

            // Relationships
            this.HasOptional(t => t.Blog)
                .WithMany(t => t.BlogStars)
                .HasForeignKey(d => d.BlogId);
            this.HasOptional(t => t.PhiUser)
                .WithMany(t => t.BlogStars)
                .HasForeignKey(d => d.UserId);

        }
    }
}
