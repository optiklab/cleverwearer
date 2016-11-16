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

    using Phi.Models.Models;

    public interface IDataStore
    {
        IList<Location> GetAllLocations();
        Location GetLocationByWOEID(string woeid);
        Location GetLocationById(Int32 id);
        IEnumerable<Location> GetLocationsByCountry(string country);
        IEnumerable<Location> GetLocationsByCity(string city, int take = 10);
        IEnumerable<Location> GetLocations(string country, string city, string region);
        IEnumerable<Location> GetLocations(string city);

        void Insert(Blog blog);
        List<Blog> GetBlogs(int limit, int languageId, params string[] themes);
        List<Blog> GetBlogs(string providerName, string header);
        bool IsNeedToUpdateBlog(string theme, int hours);
        bool IsNeedToUpdate(int hours);

        void Insert(Location location);
        void Update(Location location);
        void Delete(Location location);

        IList<Unit> GetAllUnits();
        Unit GetUnitById(Int32 id);
        Unit GetUnitByName(string name, int languageId);
        void Insert(Unit unit);
        void Update(Unit unit);
        void Delete(Unit unit);

        IList<WeatherCondition> GetAllWeatherConditions();
        WeatherCondition GetWeatherConditionById(Int32 id);
        WeatherCondition GetLastWeatherConditionByLocation(Int32 locationId);
        void Insert(WeatherCondition unit);
        void Update(WeatherCondition unit);
        void Delete(WeatherCondition unit);

        IList<Language> GetAllLanguages();
        Language GetLanguageById(Int32 id);
        Language GetLanguageByCode(string code);
        void Insert(Language language);
        void Update(Language language);
        void Delete(Language language);

        IList<ItemType> GetAllItemTypes();
        ItemType GetItemTypeById(Int32 id);
        ItemType GetItemTypeByName(string name);
        void Insert(ItemType language);
        void Update(ItemType language);
        void Delete(ItemType language);
        void UpdateShowedTimes(IEnumerable<int> itemIds);

        IList<DataProvider> GetAllDataProviders();
        DataProvider GetDataProviderById(Int32 id);
        DataProvider GetDataProviderByName(string name);
        void Insert(DataProvider dataProvider);
        void Update(DataProvider dataProvider);
        void Delete(DataProvider dataProvider);

        IList<Factor> GetAllFactors();
        Factor GetFactorById(Int32 id);

        IList<FactorType> GetAllFactorTypes();
        FactorType GetFactorTypeById(Int32 id);
        FactorType GetFactorTypeByName(string name);

        IList<Rule> GetAllRules();
        IList<Rule> GetAllRules(Int32 factorType);
        Rule GetRuleById(Int32 id);

        IList<GoodThought> GetAllGoodThoughts();
        GoodThought GetRandomGoodThought(Int32 languageId);

        IList<ActionType> GetAllActionTypes(Int32 languageId);
        IList<ActionType> GetAllActiveActionTypes(Int32 languageId);
        ActionType GetActionTypeById(Int32 id);

        IList<SuggestionTerm> GetAllSuggestionTerms(Int32 languageId);
        SuggestionTerm GetSuggestionTermById(Int32 id);
        SuggestionTerm GetSuggestionTermByCode(string code, int languageId);
        SuggestionTerm GetSuggestionTermByName(string name, int languageId);
        IList<SuggestionTerm> GetSuggestionTermBySumValue(Int32 value, int languageId);

        IList<Suggestion> GetAllSuggestions();
        Suggestion GetSuggestionById(Int32 id);
        void Insert(Suggestion suggestion);
        void Update(Suggestion suggestion);
        void Delete(Suggestion suggestion);

        IList<SeasonType> GetAllSeasonTypes();

        IList<int> GetAllPublicItemProviders(Int32 locationId);
        IList<ItemProvider> GetAllItemProviders();
        ItemProvider GetItemProviderById(Int32 id);
        ItemProvider GetItemProviderByName(string name);
        void Insert(ItemProvider itemProvider);
        void Update(ItemProvider itemProvider);
        void Delete(ItemProvider itemProvider);

        IList<int> GetAllAvailableItemIds();
        ProvidersItem GetProviderViaItemById(Int32 id);
        void Insert(ProvidersItem itemProvider);
        void Update(ProvidersItem itemProvider);
        void Delete(ProvidersItem itemProvider);

        IList<Item> GetItemsByProviderId(Int32 providerId);
        IList<Item> GetAllItems();
        Item GetItemById(Int32 id);
        Item GetItemByName(string name);
        IList<Item> GetItemsBySuggestionTerm(Int32 termId, int languageId);
        void Insert(Item item);
        void Update(Item item);
        void Delete(Item item);

        IList<ItemParameter> GetAllItemParameters();
        ItemParameter GetItemParameterById(Int32 id);
        ItemParameter GetItemParameterByName(string name);

        IList<ItemsViaParameter> GetAllItemsViaParameter();
        ItemsViaParameter GetItemsViaParameterById(Int32 id);

        IList<Image> GetAllImages();
        Image GetImageById(Int32 id);
        IList<Image> GetImagesByItemId(Int32 itemId);
        void Insert(Image item);
        void Update(Image item);
        void Delete(Image item);

        IList<ConditionDescription> GetAllConditionDescriptions();
        ConditionDescription GetConditionDescriptionByExtId(Int32 extId, int languageId);

        IList<UserProfilesViaItemProvider> GetAllUserProfilesViaItemProviders();
        UserProfilesViaItemProvider GetUserProfilesViaItemProvider(Int32 id);
        IList<UserProfilesViaItemProvider> GetUserProfilesByItemProvider(Int32 itemProviderId);
        IList<UserProfilesViaItemProvider> GetItemProvidersByUserProfile(string userId);
        void Insert(UserProfilesViaItemProvider suggestion);
        void Update(UserProfilesViaItemProvider suggestion);
        void Delete(UserProfilesViaItemProvider suggestion);

        void Insert(UserStat statistic);
    }
}
