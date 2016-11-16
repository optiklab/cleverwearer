using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class EmailAccountMap : EntityTypeConfiguration<EmailAccount>
    {
        public EmailAccountMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.DisplayName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Host)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Username)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("EmailAccount");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.DisplayName).HasColumnName("DisplayName");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Host).HasColumnName("Host");
            this.Property(t => t.Port).HasColumnName("Port");
            this.Property(t => t.Username).HasColumnName("Username");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.EnableSsl).HasColumnName("EnableSsl");
            this.Property(t => t.UseDefaultCredentials).HasColumnName("UseDefaultCredentials");
        }
    }
}
