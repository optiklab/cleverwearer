using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class FactorMap : EntityTypeConfiguration<Factor>
    {
        public FactorMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Factor");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.FactorTypeId).HasColumnName("FactorTypeId");
            this.Property(t => t.ActionTypeId).HasColumnName("ActionTypeId");

            // Relationships
            this.HasOptional(t => t.ActionType)
                .WithMany(t => t.Factors)
                .HasForeignKey(d => d.ActionTypeId);
            this.HasOptional(t => t.FactorType)
                .WithMany(t => t.Factors)
                .HasForeignKey(d => d.FactorTypeId);

        }
    }
}
