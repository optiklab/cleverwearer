/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.Repository.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Phi.Models.Models;
    using Phi.Repository.Stores;
    using Phi.Repository.Enums;

    public static class DataHelper
    {
        /// <summary>
        /// If city location can be found just by Country and City -> OK. If not, method tries to use region.
        /// </summary>
        public static Location TryToGetLocation(string woeid, string country, string city, string region, IDataStore dataStore)
        {
            if (!string.IsNullOrEmpty(woeid))
            {
               return  dataStore.GetLocationByWOEID(woeid);
            }

            if (!string.IsNullOrEmpty(country) && !string.IsNullOrEmpty(city))
            {
                var cities = dataStore.GetLocations(country, city, null);
                if (cities.Any())
                {
                    if (cities.Count() == 1)
                    {
                        var location = cities.FirstOrDefault();
                        if (location != null)
                        {
                            return location;
                        }
                    }
                    else if (!String.IsNullOrEmpty(region))
                    {
                        var exactCities = dataStore.GetLocations(country, city, region);

                        if (exactCities.Count() < 2)
                        {
                            var location = cities.FirstOrDefault();
                            if (location != null)
                            {
                                return location;
                            }
                        }
                    }
                }
                else
                {
                    // For cases of Yahoo: found Keras, Indonesia, but Weather Conditions is for Keras, Republik Indonesia
                    cities = dataStore.GetLocations(city);

                    if (cities.Count() == 1)
                    {
                        var location = cities.FirstOrDefault();
                        if (location != null)
                        {
                            return location;
                        }
                    }
                }
            }

            return null;
        }

        public static UserProfilesViaItemProvider TryToInitItemProviderForUser(string companyName, string address, string email, string phone, int locationId, string userId, IDataStore dataStore)
        {
            UserProfilesViaItemProvider link = null;
            using (var dbContextTransaction = ModelContainer.Instance.GetInstance<phiContext>().Database.BeginTransaction())
            {
                try
                {
                    var itemProvider = new ItemProvider
                    {
                        Name = companyName,
                        PhisicalAddress = address,
                        Email = email,
                        Phone = phone,
                        LocationId = locationId,
                        EnumType = (int)ItemProviderType.User
                    };

                    dataStore.Insert(itemProvider);

                    link = new UserProfilesViaItemProvider
                    {
                        PhiUserId = userId,
                        ItemProviderId = itemProvider.Id
                    };

                    dataStore.Insert(link);

                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }

            return link;
        }
    }
}
