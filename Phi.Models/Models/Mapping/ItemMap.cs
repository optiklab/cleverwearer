using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class ItemMap : EntityTypeConfiguration<Item>
    {
        public ItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(255);

            this.Property(t => t.Description)
                .HasMaxLength(2000);

            this.Property(t => t.MadeBy)
                .HasMaxLength(1000);

            this.Property(t => t.ProvideBy)
                .HasMaxLength(1000);

            this.Property(t => t.DefaultImageUri)
                .HasMaxLength(2000);

            this.Property(t => t.Referrer)
                .HasMaxLength(2000);

            // Table & Column Mappings
            this.ToTable("Item");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.MadeBy).HasColumnName("MadeBy");
            this.Property(t => t.ProvideBy).HasColumnName("ProvideBy");
            this.Property(t => t.SuggestionTerms).HasColumnName("SuggestionTerms");
            this.Property(t => t.LanguageId).HasColumnName("LanguageId");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.Season).HasColumnName("Season");
            this.Property(t => t.WaterProtectionPercent).HasColumnName("WaterProtectionPercent");
            this.Property(t => t.IceProtectionPercent).HasColumnName("IceProtectionPercent");
            this.Property(t => t.ArmoringPercent).HasColumnName("ArmoringPercent");
            this.Property(t => t.MinAge).HasColumnName("MinAge");
            this.Property(t => t.MaxAge).HasColumnName("MaxAge");
            this.Property(t => t.SunProtectionPercent).HasColumnName("SunProtectionPercent");
            this.Property(t => t.ActionTypeId).HasColumnName("ActionTypeId");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.ItemTypeId).HasColumnName("ItemTypeId");
            this.Property(t => t.IsPublic).HasColumnName("IsPublic");
            this.Property(t => t.Root).HasColumnName("Root");
            this.Property(t => t.DefaultImageUri).HasColumnName("DefaultImageUri");
            this.Property(t => t.IsChild).HasColumnName("IsChild");
            this.Property(t => t.IsAvailable).HasColumnName("IsAvailable");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.Referrer).HasColumnName("Referrer");
            this.Property(t => t.Currency).HasColumnName("Currency");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.AvailableTill).HasColumnName("AvailableTill");
            this.Property(t => t.Likes).HasColumnName("Likes");
            this.Property(t => t.IsWardrobe).HasColumnName("IsWardrobe");
            this.Property(t => t.ShowedTimes).HasColumnName("ShowedTimes");
            
            // Relationships
            this.HasOptional(t => t.ActionType)
                .WithMany(t => t.Items)
                .HasForeignKey(d => d.ActionTypeId);
            this.HasOptional(t => t.Language)
                .WithMany(t => t.Items)
                .HasForeignKey(d => d.LanguageId);
            this.HasOptional(t => t.ItemType)
                .WithMany(t => t.Items)
                .HasForeignKey(d => d.ItemTypeId);

        }
    }
}
