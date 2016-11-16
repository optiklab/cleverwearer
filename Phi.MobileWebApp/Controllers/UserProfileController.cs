/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.MobileWebApp.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using Microsoft.AspNet.Identity;
    using Phi.Models.Models;
    using Phi.Repository.Stores;
    using Phi.MobileWebApp.Models;
    using Phi.Repository.Helpers;

    [Authorize]
    public class UserProfileController : BaseController
    {
        #region Private fields

        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IDataStore _dataStore;
        private readonly IUserStore<PhiUser> _userStore;
        private readonly IUserProfileStore _userProfileStore;

        #endregion

        #region Constructor

        public UserProfileController(IDataStore dataStore, IUserStore<PhiUser> userStore, IUserProfileStore userProfileStore)
        {
            this._dataStore = dataStore;
            this._userStore = userStore;
            this._userProfileStore = userProfileStore;
        }

        #endregion

        #region Public methods

        [HttpGet]
        public new ActionResult Profile()
        {
            BundleConfig.RegisterProfileBundles(BundleTable.Bundles);

            ProfileViewModel viewModel = null;

            try
            {
                string userId = User.Identity.GetUserId();
                string userName = User.Identity.GetUserName();
                PhiUser currentUser = _userStore.FindByNameAsync(userName).Result;
                UserProfile profile = _userProfileStore.GetUserProfileById(userId);

                if (currentUser != null)
                {
                    if (profile == null)
                    {
                        this._userProfileStore.Insert(new UserProfile
                        {
                            PhiUserId = currentUser.Id,
                            NotifyMeAboutSuddenWeatherEvents = true // True by default
                        });

                        profile = _userProfileStore.GetUserProfileById(User.Identity.GetUserId());
                    }

                    var location = _GetLocationDescription(profile.LocationId);

                    viewModel = new ProfileViewModel
                    {
                        FirstName = currentUser.FirstName,
                        LastName = currentUser.LastName,
                        UserName = currentUser.UserName,
                        DateCreated = currentUser.DateCreated,
                        Email = currentUser.Email,
                        Gender = profile.Gender,
                        AvatarPictureUrl = profile.AvatarPictureUrl,
                        Location = location,
                        IsCheckedLocation = false
                    };
                    _GatherStatistics(userId, currentUser.UserName, currentUser.Email, "Profile");
                }
                else
                {
                    _GatherStatistics(userId, userName, null, "Profile");
                }
            }
            catch(Exception ex)
            {
                _logger.Error("Exception HttpGet Profile", ex);
            }

            if (viewModel != null)
            {
                return View(viewModel);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public new ActionResult Profile(ProfileViewModel model)
        {
            try
            {
                PhiUser currentUser = _userStore.FindByNameAsync(User.Identity.GetUserName()).Result;

                if (ModelState.IsValid)
                {
                    UserProfile profile = _userProfileStore.GetUserProfileById(User.Identity.GetUserId());

                    if (currentUser != null)
                    {
                        currentUser.FirstName = model.FirstName;
                        currentUser.LastName = model.LastName;
                        currentUser.UserName = model.UserName;
                    }

                    this._userStore.UpdateAsync(currentUser);

                    if (profile != null)
                    {
                        profile.Gender = profile == null ? null : model.Gender;

                        // Save location.
                        if (model.IsCheckedLocation)
                        {
                            var locationParts = model.Location.Split(',');

                            if (locationParts != null && locationParts.Length >= 2)
                            {
                                var location = DataHelper.TryToGetLocation(null, locationParts[0].Trim(), locationParts[1].Trim(),
                                    (locationParts.Length >= 2 && locationParts[2] != null) ? locationParts[2].Trim() : null, _dataStore);
                                if (location != null)
                                {
                                    profile.LocationId = location.Id;
                                }
                            }
                        }
                    }

                    this._userProfileStore.Update(profile);

                    return RedirectToAction("AdvancedProfile");
                }

                if (currentUser != null)
                {
                    model.Url = "";

                    // This data cannot come from POST because we use non-editing controls for it.
                    model.Email = currentUser.Email;
                    model.DateCreated = currentUser.DateCreated;

                    if (currentUser.UserProfile != null)
                    {
                        model.AvatarPictureUrl = currentUser.UserProfile.AvatarPictureUrl;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Exception HttpPost Profile", ex);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult AdvancedProfile()
        {
            BundleConfig.RegisterProfileBundles(BundleTable.Bundles);

            try
            {
                string userId = User.Identity.GetUserId();
                string userName = User.Identity.GetUserName();
                PhiUser currentUser = _userStore.FindByNameAsync(userName).Result;
                UserProfile profile = _userProfileStore.GetUserProfileById(userId);

                if (currentUser != null)
                {
                    return View(new AdvancedProfileViewModel
                    {
                        CompanyName = profile.CompanyName,
                        CompanyCEO = profile.CompanyCEO,
                        CompanyEmail = profile.CompanyEmail,
                        CompanyFax = profile.CompanyFax,
                        CompanyPhone = profile.CompanyPhone,
                        MainCompanyUrl = profile.MainCompanyUrl,
                        SellCompanyUrl = profile.SellCompanyUrl,
                        AdditionalInfo = profile.AdditionalInfo,
                        NotifyAboutWeatherEvents = profile.NotifyMeAboutSuddenWeatherEvents
                    });

                    _GatherStatistics(userId, currentUser.UserName, currentUser.Email, "AdvancedProfile");
                }
                else
                {
                    _GatherStatistics(userId, userName, null, "AdvancedProfile");
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Exception HttpGet AdvancedProfile", ex);
            }            

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdvancedProfile(AdvancedProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UserProfile profile = _userProfileStore.GetUserProfileById(User.Identity.GetUserId());

                    if (profile != null)
                    {
                        profile.CompanyName = model.CompanyName;
                        profile.CompanyCEO = model.CompanyCEO;
                        profile.CompanyEmail = model.CompanyEmail;
                        profile.CompanyFax = model.CompanyFax;
                        profile.CompanyPhone = model.CompanyPhone;
                        profile.MainCompanyUrl = model.MainCompanyUrl;
                        profile.SellCompanyUrl = model.SellCompanyUrl;
                        profile.AdditionalInfo = model.AdditionalInfo;
                        profile.NotifyMeAboutSuddenWeatherEvents = model.NotifyAboutWeatherEvents;
                    }

                    this._userProfileStore.Update(profile);
                }
                catch (Exception ex)
                {
                    _logger.Error("Exception HttpPost AdvancedProfile", ex);
                }     

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public ActionResult Cancel()
        {
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Private methods

        private string _GetLocationDescription(int? locationId)
        {
            if (locationId != null)
            {
                Location location = this._dataStore.GetLocationById(locationId.Value);
                if (location != null)
                {
                    return location.Country + ", " + location.Town + (String.IsNullOrEmpty(location.Admin) ? String.Empty : ", " + location.Admin) +
                        (String.IsNullOrEmpty(location.Admin2) ? String.Empty : ", " + location.Admin2) + (String.IsNullOrEmpty(location.Admin3) ? String.Empty : ", " + location.Admin3);
                }
            }

            return String.Empty;
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
                    Browser = HttpContext.Request.Browser.Browser,
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