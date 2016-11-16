/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.Repository.Stores
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Phi.Models.Models;

    public class DataStore : IDataStore
    {
        #region Private fields

        private IRepository<UserProfilesViaItemProvider> _profilesViaProvidersRepository;
        private IRepository<GoodThought> _goodThoughtsRepository;
        private IRepository<Location> _locationsRepository;
        private IRepository<Language> _languagesRepository;
        private IRepository<DataProvider> _dataProvidersRepository;
        private IRepository<Unit> _unitsRepository;
        private IRepository<Factor> _factorsRepository;
        private IRepository<FactorType> _factorTypesRepository;
        private IRepository<Suggestion> _suggestionsRepository;
        private IRepository<SuggestionItem> _suggestionItemsRepository;
        private IRepository<SuggestionTerm> _suggestionTermsRepository;
        private IRepository<ActionType> _actionTypesRepository;
        private IRepository<Item> _itemsRepository;
        private IRepository<ItemParameter> _itemParametersRepository;
        private IRepository<ItemProvider> _itemProvidersRepository;
        private IRepository<ItemsViaParameter> _itemsViaParametersRepository;
        private IRepository<ProvidersItem> _providersItemsRepository;
        private IRepository<Rule> _rulesRepository;
        private IRepository<Image> _imagesRepository;
        private IRepository<WeatherCondition> _weatherConditionRepository;
        private IRepository<ConditionDescription> _conditionDescriptionRepository;
        private IRepository<SeasonType> _seasonTypeRepository;
        private IRepository<ItemType> _itemTypesRepository;
        private IRepository<UserStat> _statisticRepository;
        private IRepository<Blog> _blogRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DataStore(
            IRepository<Location> locationsRepository,
            IRepository<Language> languagesRepository,
            IRepository<DataProvider> dataProvidersRepository,
            IRepository<Unit> unitsRepository,
            IRepository<Factor> factorsRepository,
            IRepository<FactorType> factorTypesRepository,
            IRepository<Suggestion> suggestionsRepository,
            IRepository<SuggestionItem> suggestionItemsRepository,
            IRepository<SuggestionTerm> suggestionTermsRepository,
            IRepository<ActionType> actionTypesRepository,
            IRepository<Item> itemsRepository,
            IRepository<ItemParameter> itemParametersRepository,
            IRepository<ItemProvider> itemProvidersRepository,
            IRepository<ItemsViaParameter> itemsViaParametersRepository,
            IRepository<ProvidersItem> providersItemsRepository,
            IRepository<Rule> rulesRepository,
            IRepository<Image> imagesRepository,
            IRepository<WeatherCondition> weatherConditionRepository,
            IRepository<ConditionDescription> conditionDescriptionRepository,
            IRepository<GoodThought> goodThoughtsRepository,
            IRepository<UserProfilesViaItemProvider> profilesViaProvidersRepository,
            IRepository<SeasonType> seasonTypeRepository,
            IRepository<ItemType> itemTypesRepository,
            IRepository<UserStat> statisticRepository,
            IRepository<Blog> blogRepository)
        {
            this._locationsRepository = locationsRepository;
            this._languagesRepository = languagesRepository;
            this._unitsRepository = unitsRepository;
            this._dataProvidersRepository = dataProvidersRepository;
            this._factorsRepository = factorsRepository;
            this._factorTypesRepository = factorTypesRepository;
            this._suggestionsRepository = suggestionsRepository;
            this._suggestionItemsRepository = suggestionItemsRepository;
            this._suggestionTermsRepository = suggestionTermsRepository;
            this._actionTypesRepository = actionTypesRepository;
            this._itemsRepository = itemsRepository;
            this._itemParametersRepository = itemParametersRepository;
            this._itemProvidersRepository = itemProvidersRepository;
            this._itemsViaParametersRepository = itemsViaParametersRepository;
            this._providersItemsRepository = providersItemsRepository;
            this._rulesRepository = rulesRepository;
            this._imagesRepository = imagesRepository;
            this._weatherConditionRepository = weatherConditionRepository;
            this._conditionDescriptionRepository = conditionDescriptionRepository;
            this._goodThoughtsRepository = goodThoughtsRepository;
            this._profilesViaProvidersRepository = profilesViaProvidersRepository;
            this._seasonTypeRepository = seasonTypeRepository;
            this._itemTypesRepository = itemTypesRepository;
            this._statisticRepository = statisticRepository;
            this._blogRepository = blogRepository;
        }

        #endregion

        #region Blog

        public void Insert(Blog blog)
        {
            if (blog == null)
            {
                throw new ArgumentNullException("blog");
            }

            blog.Created = DateTime.UtcNow;

            this._blogRepository.Insert(blog);
        }

        public List<Blog> GetBlogs(int limit, int languageId, params string[] themes)
        {
            if (themes == null)
            {
                return null;
            }

            return this._blogRepository.Table.Where(x => themes.Contains(x.Theme) && x.LanguageId == languageId).OrderByDescending(x => x.Created).Take(limit).ToList();
        }

        public List<Blog> GetBlogs(string providerName, string header)
        {
            if (String.IsNullOrEmpty(providerName) || String.IsNullOrEmpty(header))
            {
                return null;
            }

            return this._blogRepository.Table.Where(x => x.ProviderName == providerName && x.Header == header).OrderBy(x => x.Id).ToList();
        }

        public bool IsNeedToUpdateBlog(string providerName, int hours)
        {
            DateTime max = this._blogRepository.Table.Where(x => x.ProviderName == providerName).Where(x => x.Created.HasValue).Max(x => x.Created.Value);

            if (max.AddHours(hours) > DateTime.UtcNow)
                return false;

            return true;
        }

        public bool IsNeedToUpdate(int hours)
        {
            if (this._blogRepository.Table.Any(x => x.Created.HasValue))
            {
                DateTime max = this._blogRepository.Table.Max(x => x.Created.Value);

                if (max.AddHours(hours) > DateTime.UtcNow)
                    return false;
            }

            return true;
        }

        #endregion

        #region Locations

        /// <summary>
        /// Gets all locations.
        /// </summary>
        /// <returns></returns>
        public IList<Location> GetAllLocations()
        {
            return this._locationsRepository.Table.OrderBy(x => x.WOEID).ToList();
        }

        /// <summary>
        /// Gets the location by woeid.
        /// </summary>
        /// <param name="woeid">The woeid.</param>
        /// <returns></returns>
        public Location GetLocationByWOEID(string woeid)
        {
            if (String.IsNullOrEmpty(woeid))
            {
                return null;
            }
            
            return this._locationsRepository.Table.FirstOrDefault(x => x.WOEID == woeid);
        }

        /// <summary>
        /// Gets the location by country.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <returns></returns>
        public IEnumerable<Location> GetLocationsByCountry(string country)
        {
            if (String.IsNullOrEmpty(country))
            {
                return null;
            }
            
            return this._locationsRepository.Table.Where(x => x.Country == country).OrderBy(x => x.Id);
        }

        /// <summary>
        /// Gets the location by city.
        /// </summary>
        /// <param name="city">The city.</param>
        /// <returns></returns>
        public IEnumerable<Location> GetLocationsByCity(string city, int take = 10)
        {
            if (String.IsNullOrEmpty(city))
            {
                return null;
            }

            return this._locationsRepository.Table.Where(x => x.Town.Contains(city)).Take(take).OrderBy(x => x.Id).ToList();
        }

        public IEnumerable<Location> GetLocations(string country, string city, string region)
        {
            if (String.IsNullOrEmpty(city) || String.IsNullOrEmpty(country))
            {
                return null;
            }
            
            return this._locationsRepository.Table.Where(x => x.Town == city && x.Country == country && (String.IsNullOrEmpty(region) ? true : (region == x.Admin || region == x.Admin2 || region == x.Admin3)));
        }

        public IEnumerable<Location> GetLocations(string city)
        {
            if (String.IsNullOrEmpty(city))
            {
                return null;
            }

            return this._locationsRepository.Table.Where(x => x.Town == city).Take(10).OrderBy(x => x.Id);
        }

        /// <summary>
        /// Gets the location by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Location GetLocationById(Int32 id)
        {
            if (id <= 0)
            {
                return null;
            }
            
            return this._locationsRepository.Table.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Inserts the specified location.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <exception cref="System.ArgumentNullException">location</exception>
        public void Insert(Location location)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location");
            }

            this._locationsRepository.Insert(location);
        }

        /// <summary>
        /// Updates the specified location.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <exception cref="System.ArgumentNullException">location</exception>
        public void Update(Location location)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location");
            }

            this._locationsRepository.Update(location);
        }

        /// <summary>
        /// Deletes the specified location.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <exception cref="System.ArgumentNullException">location</exception>
        public void Delete(Location location)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location");
            }

            this._locationsRepository.Delete(location);
        }

        #endregion

        #region Weather Condition

        /// <summary>
        /// Gets all weather conditions.
        /// </summary>
        /// <returns></returns>
        public IList<WeatherCondition> GetAllWeatherConditions()
        {
            return this._weatherConditionRepository.Table.OrderBy(x => x.Id).ToList();
        }

        /// <summary>
        /// Gets the weather condition by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public WeatherCondition GetWeatherConditionById(Int32 id)
        {
            if (id <= 0)
            {
                return null;
            }

            return this._weatherConditionRepository.Table.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Gets the weather condition by location.
        /// </summary>
        /// <param name="locationId">The location identifier.</param>
        /// <returns></returns>
        public WeatherCondition GetLastWeatherConditionByLocation(Int32 locationId)
        {
            if (locationId <= 0)
            {
                return null;
            }
            
            return this._weatherConditionRepository.Table.Where(x => x.LocationId == locationId).OrderByDescending(y => y.GenereationDate).FirstOrDefault();
        }

        /// <summary>
        /// Inserts the specified weather condition.
        /// </summary>
        /// <param name="weatherCondition">The weather condition.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public void Insert(WeatherCondition weatherCondition)
        {
            if (weatherCondition == null)
            {
                throw new ArgumentNullException("weatherCondition");
            }

            this._weatherConditionRepository.Insert(weatherCondition);
        }

        /// <summary>
        /// Updates the specified weather condition.
        /// </summary>
        /// <param name="weatherCondition">The weather condition.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public void Update(WeatherCondition weatherCondition)
        {
            if (weatherCondition == null)
            {
                throw new ArgumentNullException("weatherCondition");
            }

            this._weatherConditionRepository.Update(weatherCondition);
        }

        /// <summary>
        /// Deletes the specified weather condition.
        /// </summary>
        /// <param name="weatherCondition">The weather condition.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public void Delete(WeatherCondition weatherCondition)
        {
            if (weatherCondition == null)
            {
                throw new ArgumentNullException("weatherCondition");
            }

            this._weatherConditionRepository.Delete(weatherCondition);
        }

        #endregion

        #region Units

        /// <summary>
        /// Gets all units.
        /// </summary>
        /// <returns></returns>
        public IList<Unit> GetAllUnits()
        {
            return this._unitsRepository.Table.OrderBy(x => x.Id).ToList();
        }

        /// <summary>
        /// Gets the unit by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Unit GetUnitByName(string name, int languageId)
        {
            if (String.IsNullOrEmpty(name) || languageId <= 0)
            {
                return null;
            }

            return this._unitsRepository.Table.FirstOrDefault(x => x.SystemName == name && x.LanguageId == languageId);
        }

        /// <summary>
        /// Gets the unit by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Unit GetUnitById(Int32 id)
        {
            if (id <= 0)
            {
                return null;
            }
            
            return this._unitsRepository.Table.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Inserts the specified unit.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <exception cref="System.ArgumentNullException">unit</exception>
        public void Insert(Unit unit)
        {
            if (unit == null)
            {
                throw new ArgumentNullException("unit");
            }

            this._unitsRepository.Insert(unit);
        }

        /// <summary>
        /// Updates the specified unit.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <exception cref="System.ArgumentNullException">unit</exception>
        public void Update(Unit unit)
        {
            if (unit == null)
            {
                throw new ArgumentNullException("unit");
            }

            this._unitsRepository.Update(unit);
        }

        /// <summary>
        /// Deletes the specified unit.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <exception cref="System.ArgumentNullException">unit</exception>
        public void Delete(Unit unit)
        {
            if (unit == null)
            {
                throw new ArgumentNullException("unit");
            }

            this._unitsRepository.Delete(unit);
        }

        #endregion

        #region Languages

        /// <summary>
        /// Gets all languages.
        /// </summary>
        /// <returns></returns>
        public IList<Language> GetAllLanguages()
        {
            return this._languagesRepository.Table.OrderBy(x => x.Id).ToList();
        }

        /// <summary>
        /// Gets the language by code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public Language GetLanguageByCode(string code)
        {
            if (String.IsNullOrEmpty(code))
            {
                return null;
            }

            return this._languagesRepository.Table.FirstOrDefault(x => x.Code == code);
        }

        /// <summary>
        /// Gets the language by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Language GetLanguageById(Int32 id)
        {
            if (id <= 0)
            {
                return null;
            }
            
            return this._languagesRepository.Table.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Inserts the specified language.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <exception cref="System.ArgumentNullException">language</exception>
        public void Insert(Language language)
        {
            if (language == null)
            {
                throw new ArgumentNullException("language");
            }

            this._languagesRepository.Insert(language);
        }

        /// <summary>
        /// Updates the specified language.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <exception cref="System.ArgumentNullException">language</exception>
        public void Update(Language language)
        {
            if (language == null)
            {
                throw new ArgumentNullException("language");
            }

            this._languagesRepository.Update(language);
        }

        /// <summary>
        /// Deletes the specified language.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <exception cref="System.ArgumentNullException">language</exception>
        public void Delete(Language language)
        {
            if (language == null)
            {
                throw new ArgumentNullException("language");
            }

            this._languagesRepository.Delete(language);
        }

        #endregion

        #region ItemType

        public IList<ItemType> GetAllItemTypes()
        {
            return this._itemTypesRepository.Table.OrderBy(x => x.Id).ToList();
        }

        public ItemType GetItemTypeById(Int32 id)
        {
            if (id <= 0)
            {
                return null;
            }

            return this._itemTypesRepository.Table.FirstOrDefault(x => x.Id == id);
        }

        public ItemType GetItemTypeByName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return null;
            }

            return this._itemTypesRepository.Table.FirstOrDefault(x => x.Name == name);
        }

        public void Insert(ItemType itemType)
        {
            if (itemType == null)
            {
                throw new ArgumentNullException("itemType");
            }

            this._itemTypesRepository.Insert(itemType);
        }

        public void Update(ItemType itemType)
        {
            if (itemType == null)
            {
                throw new ArgumentNullException("itemType");
            }

            this._itemTypesRepository.Update(itemType);
        }

        public void Delete(ItemType itemType)
        {
            if (itemType == null)
            {
                throw new ArgumentNullException("itemType");
            }

            this._itemTypesRepository.Delete(itemType);
        }

        #endregion

        #region DataProviders

        /// <summary>
        /// Gets all dataProviders.
        /// </summary>
        /// <returns></returns>
        public IList<DataProvider> GetAllDataProviders()
        {
            return this._dataProvidersRepository.Table.OrderBy(x => x.Id).ToList();
        }

        /// <summary>
        /// Gets the dataProvider by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public DataProvider GetDataProviderByName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return null;
            }            

            return this._dataProvidersRepository.Table.FirstOrDefault(x => x.Name == name);
        }

        /// <summary>
        /// Gets the dataProvider by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public DataProvider GetDataProviderById(Int32 id)
        {
            if (id <= 0)
            {
                return null;
            }
            
            return this._dataProvidersRepository.Table.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Inserts the specified dataProvider.
        /// </summary>
        /// <param name="dataProvider">The dataProvider.</param>
        /// <exception cref="System.ArgumentNullException">dataProvider</exception>
        public void Insert(DataProvider dataProvider)
        {
            if (dataProvider == null)
            {
                throw new ArgumentNullException("dataProvider");
            }

            this._dataProvidersRepository.Insert(dataProvider);
        }

        /// <summary>
        /// Updates the specified dataProvider.
        /// </summary>
        /// <param name="dataProvider">The dataProvider.</param>
        /// <exception cref="System.ArgumentNullException">dataProvider</exception>
        public void Update(DataProvider dataProvider)
        {
            if (dataProvider == null)
            {
                throw new ArgumentNullException("dataProvider");
            }

            this._dataProvidersRepository.Update(dataProvider);
        }

        /// <summary>
        /// Deletes the specified dataProvider.
        /// </summary>
        /// <param name="dataProvider">The dataProvider.</param>
        /// <exception cref="System.ArgumentNullException">dataProvider</exception>
        public void Delete(DataProvider dataProvider)
        {
            if (dataProvider == null)
            {
                throw new ArgumentNullException("dataProvider");
            }

            this._dataProvidersRepository.Delete(dataProvider);
        }

        #endregion

        #region Factor

        public IList<Factor> GetAllFactors()
        {
            return this._factorsRepository.Table.OrderBy(x => x.Id).ToList();
        }
        public Factor GetFactorById(Int32 id)
        {
            if (id <= 0)
            {
                return null;
            }

            return this._factorsRepository.Table.FirstOrDefault(x => x.Id == id);
        }

        #endregion

        #region FactorType

        public IList<FactorType> GetAllFactorTypes()
        {
            return this._factorTypesRepository.Table.OrderBy(x => x.Id).ToList();
        }
        public FactorType GetFactorTypeById(Int32 id)
        {
            if (id <= 0)
            {
                return null;
            }
            
            return this._factorTypesRepository.Table.FirstOrDefault(x => x.Id == id);
        }
        public FactorType GetFactorTypeByName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return null;
            }

            return this._factorTypesRepository.Table.FirstOrDefault(x => x.Name == name);
        }

        #endregion

        #region Rule

        public IList<Rule> GetAllRules()
        {
            return this._rulesRepository.Table.OrderBy(x => x.Id).ToList();
        }
        public IList<Rule> GetAllRules(Int32 factorType)
        {
            return this._rulesRepository.Table.Where(x => x.FactorTypeId == factorType).OrderBy(y => y.Id).ToList();
        }
        public Rule GetRuleById(Int32 id)
        {
            if (id <= 0)
            {
                return null;
            }
            
            return this._rulesRepository.Table.FirstOrDefault(x => x.Id == id);
        }

        #endregion

        #region ActionType

        public IList<ActionType> GetAllActionTypes(Int32 languageId)
        {
            if (languageId <= 0)
            {
                return null;
            }

            return this._actionTypesRepository.Table.Where(x => x.LanguageId.Value == languageId).OrderBy(y => y.Id).ToList();
        }
        public IList<ActionType> GetAllActiveActionTypes(Int32 languageId)
        {
            if (languageId <= 0)
            {
                return null;
            }

            return this._actionTypesRepository.Table.Where(x => x.LanguageId.Value == languageId && x.ShowOrder >= 0).OrderBy(y => y.ShowOrder).ToList();
        }
        public ActionType GetActionTypeById(Int32 id)
        {
            if (id <= 0)
            {
                return null;
            }

            return this._actionTypesRepository.Table.FirstOrDefault(x => x.Id == id);
        }

        #endregion

        #region SuggestionTerm

        public IList<SuggestionTerm> GetAllSuggestionTerms(Int32 languageId)
        {
            return this._suggestionTermsRepository.Table.Where(x => x.LanguageId == languageId).OrderBy(x => x.Id).ToList();
        }

        public SuggestionTerm GetSuggestionTermById(Int32 id)
        {
            if (id <= 0)
            {
                return null;
            }

            return this._suggestionTermsRepository.Table.FirstOrDefault(x => x.Id == id);
        }

        public SuggestionTerm GetSuggestionTermByCode(string code, int languageId)
        {
            if (String.IsNullOrEmpty(code))
            {
                return null;
            }

            return this._suggestionTermsRepository.Table.FirstOrDefault(x => x.Code == code && x.LanguageId == languageId);
        }

        public SuggestionTerm GetSuggestionTermByName(string name, int languageId)
        {
            if (String.IsNullOrEmpty(name))
            {
                return null;
            }

            return this._suggestionTermsRepository.Table.FirstOrDefault(x => x.Name == name && x.LanguageId == languageId);
        }

        /// <summary>
        /// Gets the suggestion terms by sum value with checking if every value is in the sum.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="languageId">The language identifier.</param>
        /// <returns></returns>
        public IList<SuggestionTerm> GetSuggestionTermBySumValue(Int32 sumValue, int languageId)
        {
            return this._suggestionTermsRepository.Table.Where(x => x.Value.HasValue && (x.Value & sumValue) > 0 && x.LanguageId == languageId).ToList();
        }

        #endregion

        #region Suggestion

        public IList<Suggestion> GetAllSuggestions()
        {
            return this._suggestionsRepository.Table.OrderBy(x => x.Id).ToList();
        }
        public Suggestion GetSuggestionById(Int32 id)
        {
            if (id <= 0)
            {
                return null;
            }

            var values = this._suggestionsRepository.Table.ToList();

            return values.FirstOrDefault(x => x.Id == id);
        }
        public void Insert(Suggestion suggestion)
        {
            if (suggestion == null)
            {
                throw new ArgumentNullException("suggestion");
            }

            this._suggestionsRepository.Insert(suggestion);
        }
        public void Update(Suggestion suggestion)
        {
            if (suggestion == null)
            {
                throw new ArgumentNullException("suggestion");
            }

            this._suggestionsRepository.Update(suggestion);
        }
        public void Delete(Suggestion suggestion)
        {
            if (suggestion == null)
            {
                throw new ArgumentNullException("suggestion");
            }

            this._suggestionsRepository.Delete(suggestion);
        }

        #endregion

        #region ItemProvider

        public IList<int> GetAllPublicItemProviders(Int32 locationId)
        {
            if (locationId < 1)
            {
                return null;
            }

            return this._itemProvidersRepository.Table.Where(x => x.LocationId.HasValue && x.LocationId.Value == locationId && x.IsPublic == true).Select(x => x.Id).ToList();
        }

        public IList<ItemProvider> GetAllItemProviders()
        {
            return this._itemProvidersRepository.Table.OrderBy(x => x.Id).ToList();
        }

        public ItemProvider GetItemProviderById(Int32 id)
        {
            if (id <= 0)
            {
                return null;
            }
            
            return this._itemProvidersRepository.Table.FirstOrDefault(x => x.Id == id);
        }

        public ItemProvider GetItemProviderByName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return null;
            }

            return this._itemProvidersRepository.Table.FirstOrDefault(x => x.Name == name);
        }

        /// <summary>
        /// Inserts the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="System.ArgumentNullException">item</exception>
        public void Insert(ItemProvider item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            this._itemProvidersRepository.Insert(item);
        }

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="System.ArgumentNullException">item</exception>
        public void Update(ItemProvider item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            this._itemProvidersRepository.Update(item);
        }

        /// <summary>
        /// Deletes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="System.ArgumentNullException">item</exception>
        public void Delete(ItemProvider item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            this._itemProvidersRepository.Delete(item);
        }

        #endregion

        #region ItemsViaParameter

        public IList<int> GetAllAvailableItemIds()
        {
            return this._providersItemsRepository.Table.Where(z => z.ItemId.HasValue).OrderBy(x => x.Id).Select(y => y.ItemId.Value).ToList();
        }

        public ProvidersItem GetProviderViaItemById(Int32 id)
        {
            if (id <= 0)
            {
                return null;
            }

            return this._providersItemsRepository.Table.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Inserts the specified providers item.
        /// </summary>
        /// <param name="item">The providers item.</param>
        /// <exception cref="System.ArgumentNullException">item</exception>
        public void Insert(ProvidersItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            this._providersItemsRepository.Insert(item);
        }

        /// <summary>
        /// Updates the specified providers item.
        /// </summary>
        /// <param name="item">The providers item.</param>
        /// <exception cref="System.ArgumentNullException">item</exception>
        public void Update(ProvidersItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            this._providersItemsRepository.Update(item);
        }

        /// <summary>
        /// Deletes the specified providers item.
        /// </summary>
        /// <param name="item">The providersitem.</param>
        /// <exception cref="System.ArgumentNullException">item</exception>
        public void Delete(ProvidersItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            this._providersItemsRepository.Delete(item);
        }

        #endregion

        #region Item

        public IList<Item> GetAllItems()
        {
            return this._itemsRepository.Table.OrderBy(x => x.Id).ToList();
        }

        public IList<Item> GetItemsByProviderId(Int32 providerId)
        {
            if (providerId <= 0)
            {
                return null;
            }

            return this._providersItemsRepository.Table.Where(x => x.ItemProvidersId == providerId).Select(x => GetItemById(x.ItemId.Value)).ToList();
        }

        public Item GetItemById(Int32 id)
        {
            if (id <= 0)
            {
                return null;
            }

            return this._itemsRepository.Table.FirstOrDefault(x => x.Id == id);
        }

        public Item GetItemByName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return null;
            }

            return this._itemsRepository.Table.FirstOrDefault(x => x.Name == name);
        }

        public IList<Item> GetItemsBySuggestionTerm(Int32 termId, int languageId)
        {
            if (termId <= 0 || languageId <= 0)
            {
                return null;
            }

            return this._itemsRepository.Table.Where(x => /*x.LanguageId == languageId &&*/ (x.SuggestionTerms.Value & termId) > 0).OrderBy(y => y.Id).ToList();
        }

        public void Insert(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            this._itemsRepository.Insert(item);
        }
        public void Update(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            this._itemsRepository.Update(item);
        }
        public void Delete(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            this._itemsRepository.Delete(item);
        }
        
        public void UpdateShowedTimes(IEnumerable<int> itemIds)
        {
            if (itemIds == null)
            {
                throw new ArgumentNullException("itemIds");
            }

            var items = this._itemsRepository.Table.Where(x => itemIds.Contains(x.Id)).ToList();

            foreach (var item in items)
            {
                if (item.ShowedTimes == null)
                {
                    item.ShowedTimes = 1;
                }
                else if (item.ShowedTimes + 1 == Int32.MaxValue)
                {
                    item.ShowedTimes = 0;
                }
                else
                {
                    item.ShowedTimes = item.ShowedTimes + 1;
                }

                this._itemsRepository.Update(item);
            }
        }

        #endregion

        #region ItemParameter

        public IList<ItemParameter> GetAllItemParameters()
        {
            return this._itemParametersRepository.Table.OrderBy(x => x.Id).ToList();
        }
        public ItemParameter GetItemParameterById(Int32 id)
        {
            if (id <= 0)
            {
                return null;
            }
            
            return this._itemParametersRepository.Table.FirstOrDefault(x => x.Id == id);
        }
        public ItemParameter GetItemParameterByName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return null;
            }
            
            return this._itemParametersRepository.Table.FirstOrDefault(x => x.Name == name);
        }

        #endregion

        #region ItemsViaParameter

        public IList<ItemsViaParameter> GetAllItemsViaParameter()
        {
            return this._itemsViaParametersRepository.Table.OrderBy(x => x.Id).ToList();
        }
        public ItemsViaParameter GetItemsViaParameterById(Int32 id)
        {
            if (id <= 0)
            {
                return null;
            }

            return this._itemsViaParametersRepository.Table.FirstOrDefault(x => x.Id == id);
        }

        #endregion

        #region Images

        public IList<Image> GetAllImages()
        {
            return this._imagesRepository.Table.OrderBy(x => x.Id).ToList();
        }

        public Image GetImageById(Int32 id)
        {
            if (id <= 0)
            {
                return null;
            }

            return this._imagesRepository.Table.FirstOrDefault(x => x.Id == id);
        }

        public IList<Image> GetImagesByItemId(Int32 itemId)
        {
            if (itemId <= 0)
            {
                return null;
            }

            return this._imagesRepository.Table.Where(x => x.ItemId == itemId).OrderBy(y => y.Id).ToList();
        }

        public void Insert(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            this._imagesRepository.Insert(image);
        }
        public void Update(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            this._imagesRepository.Update(image);
        }
        public void Delete(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            this._imagesRepository.Delete(image);
        }

        #endregion

        #region ConditionDescriptions

        public IList<ConditionDescription> GetAllConditionDescriptions()
        {
            return this._conditionDescriptionRepository.Table.OrderBy(x => x.Id).ToList();
        }

        public ConditionDescription GetConditionDescriptionByExtId(Int32 extId, int languageId)
        {
            if (extId <= 0 || languageId <= 0)
            {
                return null;
            }
            
            return _conditionDescriptionRepository.Table.FirstOrDefault(x => x.ExtId == extId && x.LanguageId == languageId);
        }

        #endregion

        #region Good Thoughts

        public IList<GoodThought> GetAllGoodThoughts()
        {
            return this._goodThoughtsRepository.Table.OrderBy(x => x.Id).ToList();
        }

        public GoodThought GetRandomGoodThought(Int32 languageId)
        {
            return this._goodThoughtsRepository.Table.FirstOrDefault();
        }

        #endregion

        #region UserProvile Via ItemProvider

        public IList<UserProfilesViaItemProvider> GetAllUserProfilesViaItemProviders()
        {
            return this._profilesViaProvidersRepository.Table.OrderBy(x => x.Id).ToList();
        }

        public UserProfilesViaItemProvider GetUserProfilesViaItemProvider(Int32 id)
        {
            if (id <= 0)
            {
                return null;
            }

            return this._profilesViaProvidersRepository.Table.FirstOrDefault(x => x.Id == id);
        }

        public IList<UserProfilesViaItemProvider> GetUserProfilesByItemProvider(Int32 itemProviderId)
        {
            if (itemProviderId <= 0)
            {
                return null;
            }

            return this._profilesViaProvidersRepository.Table.Where(x => x.ItemProviderId == itemProviderId).OrderBy(y => y.Id).ToList();
        }

        public IList<UserProfilesViaItemProvider> GetItemProvidersByUserProfile(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                return null;
            }

            return this._profilesViaProvidersRepository.Table.Where(x => x.PhiUserId == userId).OrderBy(y => y.Id).ToList();
        }

        /// <summary>
        /// Inserts the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="System.ArgumentNullException">item</exception>
        public void Insert(UserProfilesViaItemProvider item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            this._profilesViaProvidersRepository.Insert(item);
        }

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="System.ArgumentNullException">item</exception>
        public void Update(UserProfilesViaItemProvider item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            this._profilesViaProvidersRepository.Update(item);
        }

        /// <summary>
        /// Deletes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="System.ArgumentNullException">item</exception>
        public void Delete(UserProfilesViaItemProvider item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            this._profilesViaProvidersRepository.Delete(item);
        }

        #endregion

        #region Seasons

        public IList<SeasonType> GetAllSeasonTypes()
        {
            return this._seasonTypeRepository.Table.OrderBy(x => x.Id).ToList();
        }

        #endregion

        #region Statistics

        public void Insert(UserStat statistic)
        {
            if (statistic == null)
            {
                throw new ArgumentNullException("statistic");
            }

            this._statisticRepository.Insert(statistic);
        }

        #endregion
    }
}
