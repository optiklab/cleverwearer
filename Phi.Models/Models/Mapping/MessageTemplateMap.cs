using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class MessageTemplateMap : EntityTypeConfiguration<MessageTemplate>
    {
        public MessageTemplateMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.BccEmailAddresses)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Subject)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Body)
                .IsRequired()
                .HasMaxLength(4000);

            // Table & Column Mappings
            this.ToTable("MessageTemplate");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.BccEmailAddresses).HasColumnName("BccEmailAddresses");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.Body).HasColumnName("Body");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.EmailAccountId).HasColumnName("EmailAccountId");

            // Relationships
            this.HasOptional(t => t.EmailAccount)
                .WithMany(t => t.MessageTemplates)
                .HasForeignKey(d => d.EmailAccountId);

        }
    }
}
