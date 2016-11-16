using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class PhiUserMap : EntityTypeConfiguration<PhiUser>
    {
        public PhiUserMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.UserName)
                .HasMaxLength(256);

            this.Property(t => t.Password)
                .HasMaxLength(550);

            this.Property(t => t.ReminderQuestion)
                .HasMaxLength(255);

            this.Property(t => t.ReminderAnswer)
                .HasMaxLength(255);

            this.Property(t => t.PasswordSalt)
                .HasMaxLength(255);

            this.Property(t => t.PhoneNumber)
                .HasMaxLength(50);

            this.Property(t => t.FirstName)
                .HasMaxLength(550);

            this.Property(t => t.LastName)
                .HasMaxLength(550);

            this.Property(t => t.Email)
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("PhiUsers");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.ReminderQuestion).HasColumnName("ReminderQuestion");
            this.Property(t => t.ReminderAnswer).HasColumnName("ReminderAnswer");
            this.Property(t => t.PasswordSalt).HasColumnName("PasswordSalt");
            this.Property(t => t.PhoneNumber).HasColumnName("PhoneNumber");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.LastLoggedDate).HasColumnName("LastLoggedDate");
            this.Property(t => t.DateCreated).HasColumnName("DateCreated");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.UserType).HasColumnName("UserType");
            this.Property(t => t.UserNameFormat).HasColumnName("UserNameFormat");
            this.Property(t => t.PasswordFormat).HasColumnName("PasswordFormat");
            this.Property(t => t.EmailConfirmed).HasColumnName("EmailConfirmed");
            this.Property(t => t.PasswordHash).HasColumnName("PasswordHash");
            this.Property(t => t.SecurityStamp).HasColumnName("SecurityStamp");
            this.Property(t => t.PhoneNumberConfirmed).HasColumnName("PhoneNumberConfirmed");
            this.Property(t => t.TwoFactorEnabled).HasColumnName("TwoFactorEnabled");
            this.Property(t => t.LockoutEndDateUtc).HasColumnName("LockoutEndDateUtc");
            this.Property(t => t.LockoutEnabled).HasColumnName("LockoutEnabled");
            this.Property(t => t.AccessFailedCount).HasColumnName("AccessFailedCount");
        }
    }
}
