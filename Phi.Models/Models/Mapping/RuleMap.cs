using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class RuleMap : EntityTypeConfiguration<Rule>
    {
        public RuleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Rules");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.MinValue).HasColumnName("MinValue");
            this.Property(t => t.MaxValue).HasColumnName("MaxValue");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.FactorTypeId).HasColumnName("FactorTypeId");
            this.Property(t => t.Result).HasColumnName("Result");

            // Relationships
            this.HasOptional(t => t.FactorType)
                .WithMany(t => t.Rules)
                .HasForeignKey(d => d.FactorTypeId);

        }
    }
}
