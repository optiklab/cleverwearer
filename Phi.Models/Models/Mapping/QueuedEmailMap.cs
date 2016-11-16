using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class QueuedEmailMap : EntityTypeConfiguration<QueuedEmail>
    {
        public QueuedEmailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.FromAddress)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.FromName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.ToAddress)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.ToName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.CC)
                .IsRequired()
                .HasMaxLength(1000);

            this.Property(t => t.Bcc)
                .IsRequired()
                .HasMaxLength(1000);

            this.Property(t => t.EmailSubject)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Body)
                .IsRequired()
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("QueuedEmail");
            this.Property(t => t.Priority).HasColumnName("Priority");
            this.Property(t => t.FromAddress).HasColumnName("FromAddress");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.FromName).HasColumnName("FromName");
            this.Property(t => t.ToAddress).HasColumnName("ToAddress");
            this.Property(t => t.ToName).HasColumnName("ToName");
            this.Property(t => t.CC).HasColumnName("CC");
            this.Property(t => t.Bcc).HasColumnName("Bcc");
            this.Property(t => t.EmailSubject).HasColumnName("EmailSubject");
            this.Property(t => t.Body).HasColumnName("Body");
            this.Property(t => t.DateCreated).HasColumnName("DateCreated");
            this.Property(t => t.SentTries).HasColumnName("SentTries");
            this.Property(t => t.SentUTC).HasColumnName("SentUTC");
            this.Property(t => t.EmailAccountId).HasColumnName("EmailAccountId");

            // Relationships
            this.HasOptional(t => t.EmailAccount)
                .WithMany(t => t.QueuedEmails)
                .HasForeignKey(d => d.EmailAccountId);

        }
    }
}
