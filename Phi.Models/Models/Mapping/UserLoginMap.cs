using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class UserLoginMap : EntityTypeConfiguration<UserLogin>
    {
        public UserLoginMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LoginProvider, t.ProviderKey, t.PhiUserId });

            // Properties
            this.Property(t => t.LoginProvider)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.ProviderKey)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.PhiUserId)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("UserLogins");
            this.Property(t => t.LoginProvider).HasColumnName("LoginProvider");
            this.Property(t => t.ProviderKey).HasColumnName("ProviderKey");
            this.Property(t => t.PhiUserId).HasColumnName("PhiUserId");

            // Relationships
            this.HasRequired(t => t.PhiUser)
                .WithMany(t => t.UserLogins)
                .HasForeignKey(d => d.PhiUserId);

        }
    }
}
