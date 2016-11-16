using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class SuggestionMap : EntityTypeConfiguration<Suggestion>
    {
        public SuggestionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ShortDescription)
                .HasMaxLength(255);

            this.Property(t => t.FullDescription)
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("Suggestions");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ShortDescription).HasColumnName("ShortDescription");
            this.Property(t => t.FullDescription).HasColumnName("FullDescription");
            this.Property(t => t.WeatherConditionId).HasColumnName("WeatherConditionId");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.LanguageId).HasColumnName("LanguageId");

            // Relationships
            this.HasOptional(t => t.Language)
                .WithMany(t => t.Suggestions)
                .HasForeignKey(d => d.LanguageId);
            this.HasOptional(t => t.WeatherCondition)
                .WithMany(t => t.Suggestions)
                .HasForeignKey(d => d.WeatherConditionId);

        }
    }
}
