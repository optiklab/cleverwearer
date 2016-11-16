using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class UserProfileMap : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileMap()
        {
            // Primary Key
            this.HasKey(t => t.PhiUserId);

            // Properties
            this.Property(t => t.PhiUserId)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.AvatarPictureUrl)
                .HasMaxLength(255);

            this.Property(t => t.MainCompanyUrl)
                .HasMaxLength(255);

            this.Property(t => t.CompanyName)
                .HasMaxLength(255);

            this.Property(t => t.CompanyCEO)
                .HasMaxLength(255);

            this.Property(t => t.CompanyEmail)
                .HasMaxLength(255);

            this.Property(t => t.CompanyPhone)
                .HasMaxLength(50);

            this.Property(t => t.CompanyFax)
                .HasMaxLength(50);

            this.Property(t => t.AdditionalInfo)
                .HasMaxLength(2000);

            this.Property(t => t.SellCompanyUrl)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("UserProfile");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.LocationId).HasColumnName("LocationId");
            this.Property(t => t.PhiUserId).HasColumnName("PhiUserId");
            this.Property(t => t.AvatarPictureUrl).HasColumnName("AvatarPictureUrl");
            this.Property(t => t.IsCompany).HasColumnName("IsCompany");
            this.Property(t => t.MainCompanyUrl).HasColumnName("MainCompanyUrl");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.CompanyCEO).HasColumnName("CompanyCEO");
            this.Property(t => t.CompanyEmail).HasColumnName("CompanyEmail");
            this.Property(t => t.CompanyPhone).HasColumnName("CompanyPhone");
            this.Property(t => t.CompanyFax).HasColumnName("CompanyFax");
            this.Property(t => t.AdditionalInfo).HasColumnName("AdditionalInfo");
            this.Property(t => t.SellCompanyUrl).HasColumnName("SellCompanyUrl");
            this.Property(t => t.NotifyMeAboutSuddenWeatherEvents).HasColumnName("NotifyMeAboutSuddenWeatherEvents");

            // Relationships
            this.HasOptional(t => t.Location)
                .WithMany(t => t.UserProfiles)
                .HasForeignKey(d => d.LocationId);
            this.HasRequired(t => t.PhiUser)
                .WithOptional(t => t.UserProfile);

        }
    }
}
