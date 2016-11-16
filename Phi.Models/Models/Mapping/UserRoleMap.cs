using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class UserRoleMap : EntityTypeConfiguration<UserRole>
    {
        public UserRoleMap()
        {
            // Primary Key
            this.HasKey(t => new { t.PhiUserId, t.RoleId });

            // Properties
            this.Property(t => t.PhiUserId)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.RoleId)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("UserRoles");
            this.Property(t => t.AddRoleDate).HasColumnName("AddRoleDate");
            this.Property(t => t.PhiUserId).HasColumnName("PhiUserId");
            this.Property(t => t.RoleId).HasColumnName("RoleId");

            // Relationships
            this.HasRequired(t => t.PhiUser)
                .WithMany(t => t.UserRoles)
                .HasForeignKey(d => d.PhiUserId);
            this.HasRequired(t => t.Role)
                .WithMany(t => t.UserRoles)
                .HasForeignKey(d => d.RoleId);

        }
    }
}
