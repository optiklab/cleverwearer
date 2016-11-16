using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Phi.Models.Models.Mapping
{
    public class WeatherConditionMap : EntityTypeConfiguration<WeatherCondition>
    {
        public WeatherConditionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.FullDescription)
                .HasMaxLength(1000);

            this.Property(t => t.ShortDescription)
                .HasMaxLength(255);

            this.Property(t => t.ForecastGuid)
                .IsFixedLength()
                .HasMaxLength(36);

            this.Property(t => t.GenerationDateString)
                .HasMaxLength(255);

            this.Property(t => t.ForecastDateString)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("WeatherConditions");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Temperature).HasColumnName("Temperature");
            this.Property(t => t.TemperatureMin).HasColumnName("TemperatureMin");
            this.Property(t => t.TemperatureMax).HasColumnName("TemperatureMax");
            this.Property(t => t.FullDescription).HasColumnName("FullDescription");
            this.Property(t => t.ShortDescription).HasColumnName("ShortDescription");
            this.Property(t => t.WindSpeed).HasColumnName("WindSpeed");
            this.Property(t => t.WindDirection).HasColumnName("WindDirection");
            this.Property(t => t.AthmosphereHumidity).HasColumnName("AthmosphereHumidity");
            this.Property(t => t.AthmospherePressure).HasColumnName("AthmospherePressure");
            this.Property(t => t.AthmosphereVisibility).HasColumnName("AthmosphereVisibility");
            this.Property(t => t.AthmosphereRising).HasColumnName("AthmosphereRising");
            this.Property(t => t.Precipitation).HasColumnName("Precipitation");
            this.Property(t => t.GenereationDate).HasColumnName("GenereationDate");
            this.Property(t => t.IsForecast).HasColumnName("IsForecast");
            this.Property(t => t.ForecastGuid).HasColumnName("ForecastGuid");
            this.Property(t => t.ForecastDate).HasColumnName("ForecastDate");
            this.Property(t => t.Sunrise).HasColumnName("Sunrise");
            this.Property(t => t.Sunset).HasColumnName("Sunset");
            this.Property(t => t.GroundLevel).HasColumnName("GroundLevel");
            this.Property(t => t.UnitsId).HasColumnName("UnitsId");
            this.Property(t => t.LanguageId).HasColumnName("LanguageId");
            this.Property(t => t.SeaLevel).HasColumnName("SeaLevel");
            this.Property(t => t.IsPrecalculatedEffectiveTemperature).HasColumnName("IsPrecalculatedEffectiveTemperature");
            this.Property(t => t.EffectiveTemperature).HasColumnName("EffectiveTemperature");
            this.Property(t => t.LocationId).HasColumnName("LocationId");
            this.Property(t => t.GenerationDateString).HasColumnName("GenerationDateString");
            this.Property(t => t.ForecastDateString).HasColumnName("ForecastDateString");
            this.Property(t => t.DataProviderId).HasColumnName("DataProviderId");
            this.Property(t => t.Condition).HasColumnName("Condition");

            // Relationships
            this.HasOptional(t => t.DataProvider)
                .WithMany(t => t.WeatherConditions)
                .HasForeignKey(d => d.DataProviderId);
            this.HasOptional(t => t.Language)
                .WithMany(t => t.WeatherConditions)
                .HasForeignKey(d => d.LanguageId);
            this.HasOptional(t => t.Location)
                .WithMany(t => t.WeatherConditions)
                .HasForeignKey(d => d.LocationId);
            this.HasOptional(t => t.Unit)
                .WithMany(t => t.WeatherConditions)
                .HasForeignKey(d => d.UnitsId);

        }
    }
}
