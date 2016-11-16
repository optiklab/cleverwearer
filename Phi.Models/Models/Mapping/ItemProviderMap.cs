using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class ItemProviderMap : EntityTypeConfiguration<ItemProvider>
    {
        public ItemProviderMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(255);

            this.Property(t => t.PhisicalAddress)
                .HasMaxLength(255);

            this.Property(t => t.Email)
                .HasMaxLength(255);

            this.Property(t => t.Phone)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("ItemProviders");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.PhisicalAddress).HasColumnName("PhisicalAddress");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.LocationId).HasColumnName("LocationId");
            this.Property(t => t.IsPublic).HasColumnName("IsPublic");
            this.Property(t => t.EnumType).HasColumnName("EnumType");

            // Relationships
            this.HasOptional(t => t.Location)
                .WithMany(t => t.ItemProviders)
                .HasForeignKey(d => d.LocationId);

        }
    }
}
