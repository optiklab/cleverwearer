using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class BlogMap : EntityTypeConfiguration<Blog>
    {
        public BlogMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Theme)
                .HasMaxLength(1000);

            this.Property(t => t.Header)
                .HasMaxLength(1000);

            this.Property(t => t.Tags)
                .HasMaxLength(1000);

            this.Property(t => t.UniqueId)
                .HasMaxLength(100);

            this.Property(t => t.SourceUrl)
                .HasMaxLength(500);

            this.Property(t => t.ProviderName)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Blog");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Theme).HasColumnName("Theme");
            this.Property(t => t.Article).HasColumnName("Article");
            this.Property(t => t.Header).HasColumnName("Header");
            this.Property(t => t.Rating).HasColumnName("Rating");
            this.Property(t => t.Tags).HasColumnName("Tags");
            this.Property(t => t.PublishDate).HasColumnName("PublishDate");
            this.Property(t => t.UniqueId).HasColumnName("UniqueId");
            this.Property(t => t.SourceUrl).HasColumnName("SourceUrl");
            this.Property(t => t.LanguageId).HasColumnName("LanguageId");
            this.Property(t => t.ProviderName).HasColumnName("ProviderName");
            this.Property(t => t.Created).HasColumnName("Created");
        }
    }
}
