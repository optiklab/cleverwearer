using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class DataProviderMap : EntityTypeConfiguration<DataProvider>
    {
        public DataProviderMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(255);

            this.Property(t => t.Connection)
                .HasMaxLength(1000);

            this.Property(t => t.Code)
                .HasMaxLength(255);

            this.Property(t => t.Url)
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("DataProvider");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ConnectionType).HasColumnName("ConnectionType");
            this.Property(t => t.Connection).HasColumnName("Connection");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}
