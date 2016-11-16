using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class UserProfilesViaItemProviderMap : EntityTypeConfiguration<UserProfilesViaItemProvider>
    {
        public UserProfilesViaItemProviderMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.PhiUserId)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("UserProfilesViaItemProviders");
            this.Property(t => t.PhiUserId).HasColumnName("PhiUserId");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ItemProviderId).HasColumnName("ItemProviderId");

            // Relationships
            this.HasOptional(t => t.ItemProvider)
                .WithMany(t => t.UserProfilesViaItemProviders)
                .HasForeignKey(d => d.ItemProviderId);
            this.HasOptional(t => t.UserProfile)
                .WithMany(t => t.UserProfilesViaItemProviders)
                .HasForeignKey(d => d.PhiUserId);

        }
    }
}
