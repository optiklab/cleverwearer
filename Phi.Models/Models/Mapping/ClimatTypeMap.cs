using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class ClimatTypeMap : EntityTypeConfiguration<ClimatType>
    {
        public ClimatTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(255);

            this.Property(t => t.Belt)
                .HasMaxLength(255);

            this.Property(t => t.AlternativeNames)
                .HasMaxLength(2000);

            // Table & Column Mappings
            this.ToTable("ClimatType");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Belt).HasColumnName("Belt");
            this.Property(t => t.AlternativeNames).HasColumnName("AlternativeNames");
        }
    }
}
