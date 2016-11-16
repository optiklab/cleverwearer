using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class UserAttributeMap : EntityTypeConfiguration<UserAttribute>
    {
        public UserAttributeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(255);

            this.Property(t => t.Value)
                .HasMaxLength(255);

            this.Property(t => t.PhiUserId)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("UserAttribute");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PhiUserId).HasColumnName("PhiUserId");

            // Relationships
            this.HasOptional(t => t.PhiUser)
                .WithMany(t => t.UserAttributes)
                .HasForeignKey(d => d.PhiUserId);

        }
    }
}
