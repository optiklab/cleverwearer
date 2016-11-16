/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.MobileWebApp.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using Phi.Models.Models;
    using Phi.Repository.Stores;
    using Phi.MobileWebApp.Helpers;
    using Phi.MobileWebApp.Models;
    using System;
    using System.IO;
    using Phi.Repository;
    using Phi.Models;
    using Phi.Repository.Helpers;
    using System.Diagnostics;

    public class APIProfileController : BaseApiController
    {
        #region Private fields

        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IDataStore _dataStore;
        private readonly IUserProfileStore _userProfileStore;
        private readonly IUserStore<PhiUser> _userStore;
        private readonly IDataProvider _provider;

        #endregion

        #region Constructor

        public APIProfileController(IDataStore dataStore, IUserProfileStore userProfileStore, IUserStore<PhiUser> userStore)
        {
            this._dataStore = dataStore;
            this._userProfileStore = userProfileStore;
            this._userStore = userStore;
            this._provider = new OpenWeatherMapProvider.DataProvider(); // new YahooProvider.DataProvider();
        }

        #endregion

        #region Public methods

        [HttpPost]
        public Task<IEnumerable<UploadedFileModel>> PostAvatar()
        {
            string PATH = HttpContext.Current.Server.MapPath("~/user_images/");

            if (Request.Content.IsMimeMultipartContent())
            {
                Task<IEnumerable<UploadedFileModel>> task = null;

                try
                {
                    var streamProvider = new CustomMultipartFormDataStreamProvider(PATH, true);
                    task = Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith<IEnumerable<UploadedFileModel>>(t =>
                    {
                        if (t.IsFaulted || t.IsCanceled)
                        {
                            // TODO Log, admin email, etc.
                            throw new HttpResponseException(HttpStatusCode.InternalServerError);
                        }

                        var fileInfo = streamProvider.FileData.Select(i =>
                        {
                            FileInfo fi = new FileInfo(i.LocalFileName);
                            string path = Path.Combine("/user_images/", fi.Name);

                            try
                            {
                                var user = this._userStore.FindByIdAsync(User.Identity.GetUserId());
                                var profile = this._userProfileStore.GetUserProfileById(User.Identity.GetUserId());

                                if (profile != null)
                                {
                                    profile.AvatarPictureUrl = path;
                                    this._userProfileStore.Update(profile);

                                    return new UploadedFileModel(
                                        fi.Name,
                                        path,
                                        0);//i.Headers.ContentLength != null ? i.Headers.ContentLength.Value / 1024 : 0);
                                }
                            }
                            catch (Exception)
                            {
                            }

                            return null;
                        });
                        return fileInfo;
                    });
                }
                catch (Exception ex)
                {
                    _logger.Error("Exception API PostAvatar", ex);
                }

                return task;
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [ActionName("requestcity")]
        public IHttpActionResult RequestCity(string woeid)
        {
            Location location = null;

            try
            {
                location = _GetLocation(woeid);
            }
            catch (Exception ex)
            {
                _logger.Error("Exception API SaveCity", ex);
            }

            if (location != null)
            {
                return this.Ok(location.WOEID);
            }

            return this.NotFound();
        }

        /// <summary>
        /// Returst list of cities which is possible by requested name.
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ActionName("cities")]
        public IHttpActionResult GetSuggestedCities(string cityName)
        {
            if (String.IsNullOrEmpty(cityName) || cityName.Length > 255)
            {
                return this.NotFound();
            }

            cityName = HttpUtility.UrlDecode(cityName);

            IEnumerable<Location> locations = null;
            try
            {
                locations = this._dataStore.GetLocationsByCity(cityName);
            }
            catch (RejectedByProviderException ex)
            {
                _logger.Error("Exception API GetSuggestedCities", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error("Exception API GetSuggestedCities", ex);
                throw;
            }

            return this.Ok(locations);
        }

        [HttpGet]
        [Authorize]
        [ActionName("branchcities")]
        public IHttpActionResult GetBranchCities()
        {
            var result = new List<Location>();

            try
            {
                string userId = User.Identity.GetUserId();

                var profile = this._userProfileStore.GetUserProfileById(User.Identity.GetUserId());

                var locations = this._dataStore
                    .GetItemProvidersByUserProfile(userId)
                    .Select(x => this._dataStore.GetItemProviderById(x.ItemProviderId.Value))
                    .Select(x => x.LocationId.HasValue ? this._dataStore.GetLocationById(x.LocationId.Value) : null)
                    .Where(x => x != null)
                    .ToList();

                foreach (Location location in locations)
                {
                    result.Add(
                        new Location
                        {
                            Town = location.Town,
                            WOEID = location.WOEID,
                            Country = location.Country,
                            Admin = location.Admin,
                            Admin2 = location.Admin2,
                            Admin3 = location.Admin3
                        });
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Exception API GetBranchCities", ex);
                throw;
            }

            return this.Ok(result);
        }

        /// <summary>
        /// Gets the MOST LIKELY city WOEID.
        /// http://localhost:52965/{lang}/api/Suggestions/city?cityName=Taganrog
        /// </summary>
        /// <param name="cityName">Name of the city.</param>
        /// <returns></returns>
        /// <exception cref="RejectedByProviderException">No such city!</exception>
        [HttpGet]
        [ActionName("city")]
        [AllowAnonymous]
        public IHttpActionResult GetCityWoeid(string cityName)
        {
            if (String.IsNullOrEmpty(cityName) || cityName.Length > 255)
            {
                return this.NotFound();
            }

            cityName = HttpUtility.UrlDecode(cityName);

            Location location = null;
            if (location == null)
            {
                try
                {
                    var locations = this._dataStore.GetLocationsByCity(cityName);

                    if (locations != null && locations.Any())
                    {
                        location = locations.First();
                    }

                    if (location == null)
                    {
                        throw new RejectedByProviderException("No such city!");
                    }
                }
                catch (RejectedByProviderException ex)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return this.Ok(location.WOEID);
        }

        [HttpGet]
        [Authorize]
        [ActionName("addbranchcity")]
        public IHttpActionResult AddBranchCity(string woeid)
        {
            try
            {
                Location location = _GetLocation(woeid);

                if (location != null)
                {
                    // Link with user profile.
                    string userId = User.Identity.GetUserId();
                    var profile = this._userProfileStore.GetUserProfileById(userId);

                    // Check for duplicate location for this user profile.
                    UserProfilesViaItemProvider link = _dataStore.GetItemProvidersByUserProfile(userId)
                        .FirstOrDefault(x => this._dataStore.GetItemProviderById(x.ItemProviderId.Value).LocationId == location.Id);

                    if (link == null)
                    {
                        DataHelper.TryToInitItemProviderForUser(profile != null ? profile.CompanyName : string.Empty, "", "", "", location.Id, userId, _dataStore);

                        return this.Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Exception API AddBranchCity", ex);
            }

            return this.NotFound();
        }

        [HttpGet]
        [Authorize]
        [ActionName("removebranchcity")]
        public IHttpActionResult RemoveBranchCity(string woeid)
        {
            Location location = null;

            try
            {
                location = this._dataStore.GetLocationByWOEID(woeid);
            }
            catch (Exception ex)
            {
                _logger.Error("Exception API RemoveBranchCity", ex);
                throw;
            }

            if (location != null)
            {
                try
                {
                    // Link with user profile.
                    string userId = User.Identity.GetUserId();

                    var profile = this._userProfileStore.GetUserProfileById(User.Identity.GetUserId());

                    // Check for duplicate location for this user profile.
                    UserProfilesViaItemProvider link = _dataStore.GetItemProvidersByUserProfile(userId)
                        .FirstOrDefault(x => this._dataStore.GetItemProviderById(x.ItemProviderId.Value).LocationId == location.Id);

                    if (link != null)
                    {
                        var itemProvider = this._dataStore.GetItemProviderById(link.ItemProviderId.Value);
                        this._dataStore.Delete(itemProvider);
                        this._dataStore.Delete(link);

                        return this.Ok();
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error("Exception API RemoveBranchCity", ex);
                    throw;
                }
            }

            return this.NotFound();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Gets location from DB or get it from provider and save to DB and then return.
        /// </summary>
        /// <param name="woeid">The woeid.</param>
        /// <returns></returns>
        private Location _GetLocation(string woeid)
        {
            Location location = null;

            try
            {
                location = this._dataStore.GetLocationByWOEID(woeid);
            }
            catch (Exception)
            {
                throw;
            }

            return location;
        }

        #endregion
    }
}
