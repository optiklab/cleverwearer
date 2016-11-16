using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class UserClaimMap : EntityTypeConfiguration<UserClaim>
    {
        public UserClaimMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PhiUserId)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("UserClaims");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ClaimType).HasColumnName("ClaimType");
            this.Property(t => t.ClaimValue).HasColumnName("ClaimValue");
            this.Property(t => t.PhiUserId).HasColumnName("PhiUserId");

            // Relationships
            this.HasOptional(t => t.PhiUser)
                .WithMany(t => t.UserClaims)
                .HasForeignKey(d => d.PhiUserId);

        }
    }
}
