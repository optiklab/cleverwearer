using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class UserStatMap : EntityTypeConfiguration<UserStat>
    {
        public UserStatMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Browser)
                .HasMaxLength(255);

            this.Property(t => t.IPAddress)
                .HasMaxLength(255);

            this.Property(t => t.UserName)
                .HasMaxLength(255);

            this.Property(t => t.UserId)
                .HasMaxLength(255);

            this.Property(t => t.UserEmail)
                .HasMaxLength(255);

            this.Property(t => t.UrlReferrer)
                .HasMaxLength(1000);

            this.Property(t => t.Action)
                .HasMaxLength(255);

            this.Property(t => t.ActionPage)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("UserStat");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Browser).HasColumnName("Browser");
            this.Property(t => t.IPAddress).HasColumnName("IPAddress");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.UserEmail).HasColumnName("UserEmail");
            this.Property(t => t.DateTime).HasColumnName("DateTime");
            this.Property(t => t.UrlReferrer).HasColumnName("UrlReferrer");
            this.Property(t => t.Action).HasColumnName("Action");
            this.Property(t => t.ActionPage).HasColumnName("ActionPage");
        }
    }
}
