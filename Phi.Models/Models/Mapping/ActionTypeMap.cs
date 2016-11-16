using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class ActionTypeMap : EntityTypeConfiguration<ActionType>
    {
        public ActionTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(255);

            this.Property(t => t.Description)
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("ActionType");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.LanguageId).HasColumnName("LanguageId");
            this.Property(t => t.ShowOrder).HasColumnName("ShowOrder");

            // Relationships
            this.HasOptional(t => t.Language)
                .WithMany(t => t.ActionTypes)
                .HasForeignKey(d => d.LanguageId);

        }
    }
}
