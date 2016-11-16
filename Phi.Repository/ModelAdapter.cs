/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

using Phi.Repository.RssImporters;

namespace Phi.Repository
{
    using System;
    using System.Diagnostics;
    using Enums;
    using Helpers;
    using Models.Models;
    using Stores;

    public class ModelAdapter
    {
        /// <summary>
        /// Converts to database conditions.
        /// </summary>
        /// <param name="condition">External condition.</param>
        /// <returns></returns>
        public static WeatherCondition ConvertTo(External.WeatherCondition condition)
        {
            var result = new WeatherCondition();

            result.Temperature = condition.Temperature;
            result.TemperatureMax = condition.TemperatureMax;
            result.TemperatureMin = condition.TemperatureMin;

            // If no, get middle.
            if (result.Temperature == null && result.TemperatureMax != null && result.TemperatureMin != null)
            {
                result.Temperature = result.TemperatureMin + ((result.TemperatureMax - result.TemperatureMin) / 2);
            }

            result.IsPrecalculatedEffectiveTemperature = condition.IsPrecalculatedEffectiveTemperature;
            result.EffectiveTemperature = condition.EffectiveTemperature;
            result.ShortDescription = condition.ShortDescription;
            result.WindSpeed = condition.WindSpeed;
            result.WindDirection = condition.WindDirection;
            result.AthmosphereHumidity = condition.AthmosphereHumidity;
            result.AthmospherePressure = condition.AthmospherePressure;
            result.AthmosphereVisibility = condition.AthmosphereVisibility;
            result.AthmosphereRising = condition.AthmosphereRising;
            result.IsForecast = condition.IsForecast;
            result.ForecastDate = condition.ForecastDate;
            result.ForecastDateString = condition.ForecastDateString;
            result.ForecastGuid = condition.ForecastGuid;
            result.GenereationDate = condition.GenereationDate ?? DateTime.UtcNow;
            result.GenerationDateString = condition.GenerationDateString;
            result.FullDescription = condition.FullDescription;
            result.Precipitation = condition.Precipitation;
            result.Condition = condition.Condition;
            result.SeaLevel = condition.SeaLevel;
            result.GroundLevel = condition.GroundLevel;
            result.Sunrise = condition.Sunrise;
            result.Sunset = condition.Sunset;

            var dataStore = ModelContainer.Instance.GetInstance<IDataStore>();

            var lang = dataStore.GetLanguageByCode((condition.Language != null && condition.Language.Length > 2) ? condition.Language.Substring(0, 2) : condition.Language);
            if (lang != null)
            {
                result.LanguageId = lang.Id;

                var unit = dataStore.GetUnitByName(UnitSystems.Metric.ToString(), lang.Id);
                if (unit != null)
                {
                    result.UnitsId = unit.Id;
                }
            }

            var location = DataHelper.TryToGetLocation(condition.Woeid, condition.Country, condition.City, condition.Region, dataStore);
            if (location != null)
            {
                result.LocationId = location.Id;
            }
            else
            {
                Debug.Assert(false);
            }

            return result;
        }

        public static Blog ConvertNewsToBlog(External.News news, NewsProvider providerInfo)
        {
            return new Blog
            {
                Header = news.Header,
                Tags = news.Tags,
                Article = news.Description,
                SourceUrl = news.SourceLink,
                PublishDate = news.PublishDateTime,
                Theme = news.ThemeOrCategory,
                LanguageId = providerInfo.LanguageId,
                ProviderName = providerInfo.ProviderName,
                UniqueId = Guid.NewGuid().ToString("N")
            };
        }
    }
}
