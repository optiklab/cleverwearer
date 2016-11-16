/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
 */

using Phi.Repository.RssImporters;

namespace Phi.MobileWebApp.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using Microsoft.AspNet.Identity;
    using Phi.Repository.Services;
    using Phi.Repository.Stores;
    using Phi.MobileWebApp.Models;
    using Phi.MobileWebApp.Resources;
    using Phi.Models.Models;

    public class HomeController : BaseController
    {
        #region Private fields

        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IDataStore _dataStore;
        private readonly ISuggestionService _suggestionService;
        private readonly IUserProfileStore _userProfileStore;

        #endregion

        #region Public constructor

        public HomeController(IDataStore dataService, ISuggestionService suggestionService, IUserProfileStore userProfileStore)
        {
            this._dataStore = dataService;
            this._suggestionService = suggestionService;
            this._userProfileStore = userProfileStore;
        }

        #endregion

        #region Public methods

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult MainView()
        {
            BundleConfig.RegisterMainViewBundles(BundleTable.Bundles);

            var model = new MainViewModel();

            // Set current location.
            UserProfile profile = _userProfileStore.GetUserProfileById(User.Identity.GetUserId());
            _SetLocation(profile, ref model);

            // Fill model.
            model.Language = CurrentLang.Id;

            foreach (CommonSuggestionType itemType in Enum.GetValues(typeof(CommonSuggestionType)))
            {
                model.CommonSuggestionTypes.Add(itemType.ToString());
            }

            var blog = this._dataStore.GetBlogs(1, CurrentLang.Id, NewsTagsHelpers.GetTagDescription(NewsTags.Video));
            if (blog.Any())
            {
                model.PromoVideoDescription = blog[0].Header;
            }

            model.NewsDevicesItems = this._dataStore.GetBlogs(NewsConstants.NumberOfVisibleFeeds, CurrentLang.Id,
                NewsTagsHelpers.GetTagDescription(NewsTags.Devices),
                NewsTagsHelpers.GetTagDescription(NewsTags.AI),
                NewsTagsHelpers.GetTagDescription(NewsTags.Robots));
            model.NewsClothesItems = this._dataStore.GetBlogs(NewsConstants.NumberOfVisibleFeeds, CurrentLang.Id, NewsTagsHelpers.GetTagDescription(NewsTags.Clothes));
            model.NewsStatupsItems = this._dataStore.GetBlogs(NewsConstants.NumberOfVisibleFeeds, CurrentLang.Id, NewsTagsHelpers.GetTagDescription(NewsTags.Startups));

            model.ActionTypes = this._dataStore
                .GetAllActiveActionTypes(base.CurrentLang.Id)
                .Where(x => x.Id != 11 && x.Id != 12 && x.Id != 15 && x.Id != 16 && x.Id != 11 && x.Id != 12 && x.Id != 17 && x.Id != 18) // TODO Temporary hide redundant filters.
                .ToList();
            //model.ItemTypes = CommonHelpers.GetItemTypesForFilter(base.CurrentLang.Id);

            // Gather statistics of page visiting.
            if (profile != null && !string.IsNullOrEmpty(profile.PhiUserId))
            {
                _GatherStatistics(profile.PhiUserId, null, null, "mainView");
            }
            else
            {
                _GatherStatistics(null, null, null, "mainView");
            }

            return PartialView(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = GlobalResources.AboutDescription;

            _GatherStatistics(null, null, null, "About");

            return View();
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public ActionResult Blog()
        //{
        //    _GatherStatistics(null, null, null, "Blog");

        //    return View();
        //}

        #endregion

        #region Private methods

        private void _SetLocation(UserProfile profile, ref MainViewModel model)
        {
            try
            {                
                var selectedCityCountry = HttpContext.Request.Cookies["SelectedCityCountry"];
                var selectedWoeid = HttpContext.Request.Cookies["SelectedWoeid"];

                if (profile != null && profile.LocationId != null &&
                    (selectedCityCountry == null || String.IsNullOrEmpty(selectedCityCountry.Value)) &&
                    (selectedWoeid == null || String.IsNullOrEmpty(selectedWoeid.Value)))
                {
                    // Get locations selected by USER from Database...
                    Location location = this._dataStore.GetLocationById(profile.LocationId.Value);

                    if (location != null)
                    {
                        var cookie = new System.Web.HttpCookie("SelectedCityCountry");
                        cookie.HttpOnly = false;
                        cookie.Secure = false; // TODO AY HTTPS True if SSL
                        cookie.Value = location.Town + ", " + location.Country;
                        HttpContext.Response.SetCookie(cookie);

                        cookie = new System.Web.HttpCookie("SelectedWoeid");
                        cookie.HttpOnly = false;
                        cookie.Secure = false; // TODO AY HTTPS True if SSL
                        cookie.Value = location.WOEID;
                        HttpContext.Response.SetCookie(cookie);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Exception _SetLocation", ex);
            }
        }

        private void _GatherStatistics(string userId, string userName, string userEmail, string actionPage)
        {
            try
            {
                this._dataStore.Insert(new UserStat
                {
                    ActionPage = actionPage,
                    Action = "Visit",
                    UserId = userId,
                    UserName = userName,
                    UserEmail = userEmail,
                    Browser = (HttpContext.Request.Browser.Browser ?? string.Empty) + " " + (HttpContext.Request.Browser.Version ?? string.Empty) + " " + (HttpContext.Request.Browser.Platform ?? string.Empty),
                    DateTime = DateTime.UtcNow,
                    IPAddress = null,
                    UrlReferrer = (HttpContext.Request.UrlReferrer != null ? HttpContext.Request.UrlReferrer.AbsoluteUri : null)
                });
            }
            catch
            {

            }
        }

        #endregion
    }
}