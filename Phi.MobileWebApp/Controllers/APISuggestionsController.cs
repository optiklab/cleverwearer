/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.MobileWebApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Web;
    using System.Web.Caching;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using Phi.MobileWebApp.Helpers;
    using Phi.MobileWebApp.Models;
    using Phi.Models.Models;
    using Phi.Repository;
    using Phi.Repository.Enums;
    using Phi.Repository.Extensions;
    using Phi.Repository.Infrastructure;
    using Phi.Repository.Services;
    using Phi.Repository.Stores;
    
    public class APISuggestionsController : BaseApiController
    {
        #region Private fields

        private const string NUMERIC_FORMAT = "{0:0.#}";
        private const string ZERO = "?";
        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IDataStore _dataStore;
        private readonly ISuggestionService _suggestionService;
        private readonly IUserProfileStore _userProfileStore;
        private readonly IDataProvider _provider;

        #endregion

        #region Public constructor

        public APISuggestionsController(IDataStore dataStore, ISuggestionService suggestionService, IUserProfileStore userProfileStore)
        {
            this._dataStore = dataStore;
            this._suggestionService = suggestionService;
            this._provider = new OpenWeatherMapProvider.DataProvider();
            this._userProfileStore = userProfileStore;
        }

        #endregion

        #region Public methods

        [HttpGet]
        [AllowAnonymous]
        [ActionName("weather")]
        public WeatherConditionModel GetWeatherConditions(string woeid, DateTime? date)
        {
            var result = new WeatherConditionModel();
            IEnumerable<Repository.External.WeatherCondition> forecasts = null;

            int languageId = 2; // Russian
            if (CurrentLang != null)
            {
                languageId = CurrentLang.Id;
            }

            string key = "s" + woeid + "l" + languageId;

            try
            {
                // Don't us date because we get forecast  for all dates of the week just by woeid.
                if (HttpContext.Current.Cache[woeid] != null)
                {
                    forecasts = HttpContext.Current.Cache[key] as IEnumerable<Repository.External.WeatherCondition>;

                    if (forecasts.Count() == 0)
                    {
                        forecasts = this._provider.GetWeatherForecastByWoeid(woeid,
                            (languageId == 2) ? _provider.GetUnitsName(Units.Metric) : _provider.GetUnitsName(Units.Imperial));
                        HttpContext.Current.Cache.Add(key, forecasts, null, DateTime.MaxValue, TimeSpan.FromMinutes(20), CacheItemPriority.High, null);
                    }
                }
                else
                {
                    forecasts = this._provider.GetWeatherForecastByWoeid(woeid, (languageId == 2) ? _provider.GetUnitsName(Units.Metric) : _provider.GetUnitsName(Units.Imperial));
                    HttpContext.Current.Cache.Add(key, forecasts, null, DateTime.MaxValue, TimeSpan.FromMinutes(20), CacheItemPriority.High, null);
                }

                // If date parameter set, then select by date.
                Repository.External.WeatherCondition selectedConditions = null;
                if (date.HasValue)
                {
                    selectedConditions = forecasts.FirstOrDefault(x => x.ForecastDate.HasValue && x.ForecastDate.Value.Date == date.Value.Date);
                }
                else
                {
                    selectedConditions = forecasts.FirstOrDefault(x => !x.IsForecast);
                }

                if (selectedConditions != null)
                {
                    var weatherCondition = ModelAdapter.ConvertTo(selectedConditions);

                    if (weatherCondition != null)
                    {
                        this._dataStore.Insert(weatherCondition);
                    }
                    else
                    {
                        Debug.Assert(false);
                    }

                    result = _Convert(selectedConditions, forecasts, languageId);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Exception API Profile", ex);
            }

            return result;
        }

        /// <summary>
        /// Gets the suggestion.
        /// </summary>
        /// <param name="woeid">The woeid.</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ActionName("suggestion")]
        public FullSuggestionModel GetSuggestion(string woeid, int fresh, DateTime? date) //, string actionType = "", int page = 0)
        {
            FullSuggestionModel output = null;

            string userId = string.Empty;

            if (User != null && User.Identity != null)
            {
                userId = User.Identity.GetUserId();
            }

            int languageId = 2; // Default is Russian
            languageId = CurrentLang.Id;
            string key = "s" + woeid + "l" + languageId + "d" + (date.HasValue ? date.Value.Ticks.ToString() : string.Empty) + "u" + (userId ?? string.Empty);

            if (HttpContext.Current.Cache[key] != null)
            {
                output = HttpContext.Current.Cache[key] as FullSuggestionModel;
            }

            if (fresh == 0 && output != null && output.SuggestedItems.Any())
            {
                return output; // _RemoveRedundantPagesElements(output, actionType, page);
            }

            try
            {
                output = _CreateSuggestionsModel(woeid, languageId, userId);

                HttpContext.Current.Cache.Add(key, output, null, DateTime.MaxValue, TimeSpan.FromMinutes(20), CacheItemPriority.High, null);
            }
            catch (Exception ex)
            {
                _logger.Error("Exception API GetSuggestion", ex);
            }

            return output; // _RemoveRedundantPagesElements(output, actionType, page);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Converts weather conditions to model for showing.
        /// </summary>
        /// <param name="currentConditions">The currentConditions.</param>
        /// <returns></returns>
        private WeatherConditionModel _Convert(Repository.External.WeatherCondition currentConditions,
            IEnumerable<Repository.External.WeatherCondition> forecasts,
            int languageId)
        {
            var result = new WeatherConditionModel();

            if (currentConditions == null)
            {
                return result;
            }

            if (currentConditions.Condition.HasValue)
            {
                var iconName = this._dataStore.GetConditionDescriptionByExtId(currentConditions.Condition.Value, languageId);

                if (iconName != null)
                {
                    result.ConditionIcon = iconName.Icon;
                    result.ShortDescription = iconName.ShortDescription;
                }
            }

            var unit = _dataStore.GetUnitByName((languageId == 2) ? UnitSystems.Metric.ToString() : UnitSystems.USA.ToString(), languageId);

            if (unit != null)
            {
                result.TemperatureUnits = unit.Temperature;
                result.PressureUnits = unit.Pressure;
                result.LightUnits = unit.Light;
                result.DistanceUnits = unit.Distance;
                result.HumidityUnits = unit.Humidity;
                result.SpeedUnits = unit.Speed;
            }

            result.TemperatureMax = currentConditions.TemperatureMax.HasValue ? String.Format(NUMERIC_FORMAT, currentConditions.TemperatureMax.Value) : ZERO;
            result.TemperatureMin = currentConditions.TemperatureMin.HasValue ? String.Format(NUMERIC_FORMAT, currentConditions.TemperatureMin.Value) : ZERO;
            result.EffectiveTemperature = currentConditions.EffectiveTemperature.HasValue ? String.Format(NUMERIC_FORMAT, currentConditions.EffectiveTemperature.Value) : ZERO;

            if (currentConditions.Temperature.HasValue)
            {
                result.Temperature = String.Format(NUMERIC_FORMAT, currentConditions.Temperature.Value);
            }
            else
            {
                // Calculate middle temperature.
                if (currentConditions.TemperatureMax.HasValue && currentConditions.TemperatureMin.HasValue)
                {
                    result.Temperature = Convert.ToInt32(((currentConditions.TemperatureMax.Value + currentConditions.TemperatureMin.Value) / 2)).ToString();
                }
                else
                {
                    result.Temperature = ZERO;
                }
            }

            if (currentConditions.WindSpeed.HasValue)
            {
                result.WindSpeed = currentConditions.WindSpeed.Value.ToString();

                // https://ru.wikipedia.org/wiki/%D0%A8%D0%BA%D0%B0%D0%BB%D0%B0_%D0%91%D0%BE%D1%84%D0%BE%D1%80%D1%82%D0%B0
                result.IsWindy = (currentConditions.WindSpeed.Value >= 29);
            }
            else
            {
                result.WindSpeed = ZERO;
            }

            if (currentConditions.WindDirection.HasValue)
            {
                result.WindDirection = Phi.MobileWebApp.Helpers.CommonHelpers.GetWindDirectionByAngle(currentConditions.WindDirection.Value);
            }
            else
            {
                result.WindDirection = String.Empty;
            }

            result.AthmosphereHumidity = currentConditions.AthmosphereHumidity.HasValue ? String.Format(NUMERIC_FORMAT, currentConditions.AthmosphereHumidity.Value) : ZERO;
            result.AthmospherePressure = currentConditions.AthmospherePressure.HasValue ? String.Format(NUMERIC_FORMAT, currentConditions.AthmospherePressure.Value) : ZERO;
            result.AthmosphereVisibility = currentConditions.AthmosphereVisibility.HasValue ? String.Format(NUMERIC_FORMAT, currentConditions.AthmosphereVisibility.Value) : ZERO;
            result.AthmosphereRising = currentConditions.AthmosphereRising.HasValue ? String.Format(NUMERIC_FORMAT, currentConditions.AthmosphereRising.Value) : ZERO;
            result.IsForecast = currentConditions.IsForecast;
            result.ForecastDateString = currentConditions.ForecastDateString;

            if (currentConditions.ForecastDate.HasValue)
            {
                result.ForecastDate = currentConditions.ForecastDate.Value;
            }

            result.ForecastGuid = currentConditions.ForecastGuid;
            result.GenerationDateString = currentConditions.GenerationDateString;
            result.FullDescription = currentConditions.FullDescription;
            result.Precipitation = currentConditions.Precipitation;
            result.SeaLevel = currentConditions.SeaLevel.HasValue ? String.Format(NUMERIC_FORMAT, currentConditions.SeaLevel.Value) : ZERO;
            result.GroundLevel = currentConditions.GroundLevel.HasValue ? String.Format(NUMERIC_FORMAT, currentConditions.GroundLevel.Value) : ZERO;
            result.Sunrise = currentConditions.Sunrise.HasValue ?
                DateTimeHelpers.ToShortString(currentConditions.Sunrise.Value, languageId) :
                ZERO;
            result.Sunset = currentConditions.Sunset.HasValue ?
                DateTimeHelpers.ToShortString(currentConditions.Sunset.Value, languageId) :
                ZERO;

            // var lang = dataStore.GetLanguageByCode(condition.Language);
            result.Language = currentConditions.Language;

            // dataStore.GetLocationByCity(condition.City);
            result.Location = currentConditions.City;
            result.Latitude = currentConditions.Latitude;
            result.Longitude = currentConditions.Longitude;

            result.DataProviderName = "Hidden";

            // Fill values for forecast dates.
            if (forecasts != null)
            {
                foreach (var forecast in forecasts)
                {
                    if (forecast.ForecastDate.HasValue)
                    {
                        result.Forecasts.Add(_LocalizeWeekDay(forecast.ForecastDateString), forecast.ForecastDate.Value);
                    }
                }
            }

            return result;
        }

        private string _LocalizeWeekDay(string weekDayFromProvider)
        {
            if (weekDayFromProvider.Equals("sun", StringComparison.InvariantCultureIgnoreCase))
            {
                return Resources.GlobalResources.Sun;
            }
            else if (weekDayFromProvider.Equals("mon", StringComparison.InvariantCultureIgnoreCase))
            {
                return Resources.GlobalResources.Mon;
            }
            else if (weekDayFromProvider.Equals("tue", StringComparison.InvariantCultureIgnoreCase))
            {
                return Resources.GlobalResources.Tue;
            }
            else if (weekDayFromProvider.Equals("wed", StringComparison.InvariantCultureIgnoreCase))
            {
                return Resources.GlobalResources.Wed;
            }
            else if (weekDayFromProvider.Equals("thu", StringComparison.InvariantCultureIgnoreCase))
            {
                return Resources.GlobalResources.Thu;
            }
            else if (weekDayFromProvider.Equals("fri", StringComparison.InvariantCultureIgnoreCase))
            {
                return Resources.GlobalResources.Fri;
            }
            else if (weekDayFromProvider.Equals("sat", StringComparison.InvariantCultureIgnoreCase))
            {
                return Resources.GlobalResources.Sat;
            }
            else
            {
                Debug.Assert(false);
                return string.Empty;
            }
        }

        private FullSuggestionModel _CreateSuggestionsModel(string woeid, int languageId, string userId)
        {
            FullSuggestionModel output = new FullSuggestionModel();

            SuggestionsResult suggestionResult = this._suggestionService.GetSuggestion(woeid, languageId, userId);

            if (suggestionResult != null)
            {
                Dictionary<int, ItemType> itemTypes = this._dataStore.GetAllItemTypes().ToDictionary(k => k.Id);

                foreach (string key in suggestionResult.CommonSuggestions.Keys)
                {
                    output.CommonSuggestedItems.Add(key, suggestionResult.CommonSuggestions[key].Select(x => new SuggestionModel { Name = x }).ToList());
                }

                var itemsToUpdateShowTimes = new List<int>();

                foreach (string actionTypeName in suggestionResult.Suggestions.Keys)
                {
                    // Create HTML id from action type name.
                    string strongActionTypeName = actionTypeName.RemoveSpecialSymbols().ToLower();
                    if (!output.SuggestedItems.Keys.Contains(strongActionTypeName))
                    {
                        output.SuggestedItems.Add(strongActionTypeName, new List<SuggestionModel>());
                    }

                    var itemsCollection = suggestionResult.Suggestions[actionTypeName].SuggestionItems
                        .Where(x => x.Item.ActionTypeId.HasValue)
                        .Take(18); // <<<<< ===== TAKE ONLY 18 .

                    // Look through items to build model.
                    foreach (var item in itemsCollection)
                    {
                        string imagePath = String.Empty;                        
                        if (!string.IsNullOrEmpty(item.Item.DefaultImageUri))
                        {
                            imagePath = item.Item.DefaultImageUri;
                        }
                        else
                        {
                            var images = this._dataStore.GetImagesByItemId(item.ItemId.Value);
                            if (images.Any())
                            {
                                imagePath = images.First().ImageUrl;
                            }
                            else
                            {
                                imagePath = @"/img/default_wear/veshalka.png";
                            }
                        }

                        string itemTypeFilter = "0";
                        ItemType itemType = null;
                        if (item.Item.ItemTypeId.HasValue && itemTypes.TryGetValue(item.Item.ItemTypeId.Value, out itemType) && itemType.EnumType.HasValue)
                        {
                            itemTypeFilter = itemType.EnumType.Value.ToString();
                        }
                        
                        output.SuggestedItems[strongActionTypeName].Add(
                            new SuggestionModel
                            {
                                ActionType = actionTypeName,
                                ImageUrl = imagePath,
                                ReferrerUrl = item.Item.Referrer,
                                Price = (item.Item.Price ?? 0).ToString() + " " + ContentHelpers.GetLocalCurrencyName(item.Item.Currency),
                                Name = item.Item.Name,
                                Description = item.Item.Description,
                                MadeBy = item.Item.MadeBy,
                                ProvideBy = item.Item.ProvideBy ?? string.Empty,
                                Language = CurrentLang.Code,
                                Gender = item.Item.Gender,
                                Season = item.Item.Season.ToString(),
                                WaterProtectionPercent =
                                    item.Item.WaterProtectionPercent.HasValue
                                        ? item.Item.WaterProtectionPercent.Value
                                        : 0,
                                IceProtection =
                                    item.Item.IceProtectionPercent.HasValue
                                        ? item.Item.IceProtectionPercent.Value
                                        : false,
                                ArmoringPercent =
                                    item.Item.ArmoringPercent.HasValue
                                        ? item.Item.ArmoringPercent.Value
                                        : 0,
                                MinAge = item.Item.MinAge.HasValue ? item.Item.MinAge.Value : 0,
                                MaxAge = item.Item.MaxAge.HasValue ? item.Item.MaxAge.Value : 0,
                                SunProtectionPercent =
                                    item.Item.SunProtectionPercent.HasValue
                                        ? item.Item.SunProtectionPercent.Value
                                        : 0,
                                ItemType = itemTypeFilter,
                                IsWardrobe = item.Item.IsWardrobe
                            });
                    }

                    itemsToUpdateShowTimes.AddRange(itemsCollection.Where(x => x.ItemId.HasValue).Select(x => x.ItemId.Value));

                    if (string.IsNullOrEmpty(output.ForecastDescription))
                    {
                        output.ForecastDescription = suggestionResult.Suggestions[actionTypeName].FullDescription;
                    }
                }

                _dataStore.UpdateShowedTimes(itemsToUpdateShowTimes);
            }

            return output;
        }

        private FullSuggestionModel _RemoveRedundantPagesElements(FullSuggestionModel model, string actionType = "", int page = 0)
        {
            var dict = new Dictionary<string, IList<SuggestionModel>>();
            
            if (string.IsNullOrEmpty(actionType))
            {
                actionType = model.SuggestedItems.Keys.First();
            }

            IList<SuggestionModel> items = model.SuggestedItems[actionType].Take(page * 18 + 18).ToList();

            dict.Add(actionType, items);

            model.SuggestedItems = dict;

            return model;
        }

        #endregion
    }
}
