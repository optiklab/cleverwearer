using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class BlogCommentMap : EntityTypeConfiguration<BlogComment>
    {
        public BlogCommentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Text)
                .HasMaxLength(4000);

            this.Property(t => t.UserId)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("BlogComments");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.BlogId).HasColumnName("BlogId");

            // Relationships
            this.HasOptional(t => t.Blog)
                .WithMany(t => t.BlogComments)
                .HasForeignKey(d => d.BlogId);
            this.HasOptional(t => t.PhiUser)
                .WithMany(t => t.BlogComments)
                .HasForeignKey(d => d.UserId);

        }
    }
}
