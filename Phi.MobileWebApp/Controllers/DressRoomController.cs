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
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Phi.Repository.Stores;
    using Phi.MobileWebApp.Models;
    using Phi.Models.Models;
    using System.Transactions;
    using Phi.Repository;
    using Phi.Repository.Helpers;

    [Authorize]
    public class DressRoomController : BaseController
    {
        #region Private fields

        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IDataStore _dataStore;
        private IUserProfileStore _userProfileStore;

        #endregion

        #region Public constructor

        public DressRoomController(IDataStore dataService, IUserProfileStore userProfileStore)
        {
            this._dataStore = dataService;
            this._userProfileStore = userProfileStore;
        }

        #endregion

        #region Public methods

        [HttpGet]
        public ActionResult DressRoom()
        {
            List<ItemViewModel> models = null;

            try
            {
                string userId = User.Identity.GetUserId();

                var userItems = this._dataStore
                    .GetItemProvidersByUserProfile(userId)
                    .Select(x => this._dataStore.GetItemProviderById(x.ItemProviderId.Value))
                    .SelectMany(x => this._dataStore.GetItemsByProviderId(x.Id));

                models = new List<ItemViewModel>();

                foreach (var userItem in userItems)
                {
                    string imageUrl = "~/img/default_wear/veshalka.png";
                    var images = this._dataStore.GetImagesByItemId(userItem.Id);
                    var firstImage = images.FirstOrDefault();
                    if (firstImage != null)
                    {
                        imageUrl = firstImage.ImageUrl;
                    }

                    models.Add(new ItemViewModel
                    {
                        Id = userItem.Id,
                        Name = userItem.Name,
                        Description = userItem.Description,
                        ImageUrl = imageUrl,
                        Gender = userItem.Gender,
                        MadeBy = userItem.MadeBy,
                        ProvideBy = userItem.ProvideBy,
                        IsPublic = userItem.IsPublic != null ? userItem.IsPublic.Value : false,
                        SuggestionTerms = userItem.SuggestionTerms.HasValue ? userItem.SuggestionTerms.Value : 0,
                        Season = userItem.Season.HasValue ? userItem.Season.Value : 0,
                        WaterProtectionPercent = userItem.WaterProtectionPercent.HasValue ? userItem.WaterProtectionPercent.Value : 0,
                        IceProtection = userItem.IceProtectionPercent.HasValue ? userItem.IceProtectionPercent.Value : false,
                        ArmoringPercent = userItem.ArmoringPercent.HasValue ? userItem.ArmoringPercent.Value : 0,
                        SunProtectionPercent = userItem.SunProtectionPercent.HasValue ? userItem.SunProtectionPercent.Value : 0,
                        MinAge = userItem.MinAge.HasValue ? userItem.MinAge.Value : 0,
                        MaxAge = userItem.MaxAge.HasValue ? userItem.MaxAge.Value : 0,
                        Year = userItem.Year.HasValue ? userItem.Year.Value.Year : DateTime.MinValue.Year
                    });
                }

                _GatherStatistics(userId, "DressRoom");
            }
            catch (Exception ex)
            {
                _logger.Error("Exception HttpGet DressRoom", ex);
            }

            return View(models);
        }

        [HttpGet]
        public ActionResult EditDress(int id)
        {
            ItemViewModel model = null;

            try
            {
                Item item = _dataStore.GetItemById(id);

                if (item != null)
                {
                    model = new ItemViewModel();

                    var firstImage = _dataStore.GetImagesByItemId(item.Id).FirstOrDefault();

                    if (firstImage != null)
                    {
                        model.ImageUrl = firstImage.ImageUrl;
                    }

                    model.Id = item.Id;
                    model.Name = item.Name;
                    model.Description = item.Description;
                    model.Gender = item.Gender;
                    model.MadeBy = item.MadeBy;
                    model.ProvideBy = item.ProvideBy;
                    model.Season = item.Season;
                    model.WaterProtectionPercent = item.WaterProtectionPercent != null ? item.WaterProtectionPercent.Value : 0;
                    model.IceProtection = item.IceProtectionPercent != null ? item.IceProtectionPercent.Value : false;
                    model.ArmoringPercent = item.ArmoringPercent != null ? item.ArmoringPercent.Value : 0;
                    model.SunProtectionPercent = item.SunProtectionPercent != null ? item.SunProtectionPercent.Value : 0;
                    model.MinAge = item.MinAge != null ? item.MinAge.Value : 0;
                    model.MaxAge = item.MaxAge != null ? item.MaxAge.Value : 0;
                    model.Year = item.Year != null ? item.Year.Value.Year : 0;
                    model.IsPublic = item.IsPublic != null ? item.IsPublic.Value : false;

                    model.ActionTypeId = item.ActionTypeId != null ? item.ActionTypeId.Value : 0;
                    model.ActionTypes = _dataStore.GetAllActiveActionTypes(base.CurrentLang.Id).ToList();

                    model.SuggestionTerms = item.SuggestionTerms != null ? item.SuggestionTerms.Value : 0;

                    // We should fill required fields too.
                    if ((item.SuggestionTerms.Value & 2) > 0)
                    {
                        model.SuggestionTermsTemperature = 2;
                    }
                    else if ((item.SuggestionTerms.Value & 4) > 0)
                    {
                        model.SuggestionTermsTemperature = 4;
                    }
                    else if ((item.SuggestionTerms.Value & 8) > 0)
                    {
                        model.SuggestionTermsTemperature = 8;
                    }
                    else if ((item.SuggestionTerms.Value & 256) > 0)
                    {
                        model.SuggestionTermsTemperature = 256;
                    }
                    else if ((item.SuggestionTerms.Value & 512) > 0)
                    {
                        model.SuggestionTermsTemperature = 512;
                    }
                    else if ((item.SuggestionTerms.Value & 1024) > 0)
                    {
                        model.SuggestionTermsTemperature = 1024;
                    }
                    else if ((item.SuggestionTerms.Value & 2048) > 0)
                    {
                        model.SuggestionTermsTemperature = 2048;
                    }
                    else
                    {
                        model.SuggestionTermsTemperature = 0;
                    }
                }

                _GatherStatistics(User.Identity.GetUserId(), "EditDress");
            }
            catch (Exception ex)
            {
                _logger.Error("Exception HttpGet EditDress", ex);
            }

            if (model != null)
            {
                return View(model);
            }

            return RedirectToAction("DressRoom");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDress(ItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();

                try
                {

                    Item item = this._dataStore.GetItemById(model.Id);

                    if (item != null)
                    {
                        item.Name = model.Name;
                        item.Description = model.Description;
                        item.Gender = model.Gender != null ? model.Gender.Value : false;
                        item.MadeBy = model.MadeBy;
                        item.ProvideBy = model.ProvideBy;
                        item.SuggestionTerms = model.SuggestionTermsTemperature.Value + model.SuggestionTermsAdditional + model.SuggestionTermsExtra;
                        item.Season = model.Season.Value;
                        item.WaterProtectionPercent = model.WaterProtectionPercent;
                        item.IceProtectionPercent = model.IceProtection;
                        item.ArmoringPercent = model.ArmoringPercent;
                        item.SunProtectionPercent = model.SunProtectionPercent;
                        item.MinAge = model.MinAge;
                        item.MaxAge = model.MaxAge;
                        item.Year = new DateTime(model.Year, 1, 1);
                        item.IsPublic = model.IsPublic;
                        item.ActionTypeId = model.ActionTypeId.Value;
                        item.LanguageId = base.CurrentLang.Id; //todo

                        try
                        {
                            this._dataStore.Update(item);
                        }
                        catch (Exception ex)
                        {
                            _logger.Error("Exception HttpPost EditDress - Update Item", ex);
                        }

                        if (model.ImageReloaded != null && model.ImageReloaded.Value)
                        {
                            // Delete old picture.
                            try
                            {
                                var firstImage = this._dataStore.GetImagesByItemId(item.Id).FirstOrDefault();
                                if (firstImage != null)
                                {
                                    this._dataStore.Delete(firstImage);
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.Error("Exception HttpPost EditDress - Delete Old Image", ex);
                            }

                            // Assign new one.
                            try
                            {
                                Image image = new Image
                                {
                                    ImageUrl = model.ImageUrl,
                                    ItemId = item.Id
                                };

                                this._dataStore.Insert(image);
                            }
                            catch (Exception ex)
                            {
                                _logger.Error("Exception HttpPost EditDress - Insert New Image", ex);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error("Exception HttpPost EditDress", ex);
                }

                return RedirectToAction("DressRoom");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult DeleteDress(int id)
        {
            // TODO Not implemented.

            return RedirectToAction("DressRoom");
        }

        [HttpGet]
        public ActionResult ManageDress()
        {
            var model = new ItemViewModel();

            try
            {
                model.ActionTypes = _dataStore.GetAllActiveActionTypes(base.CurrentLang.Id).ToList();

                _GatherStatistics(User.Identity.GetUserId(), "ManageDress");
            }
            catch (Exception ex)
            {
                _logger.Error("Exception HttpGet ManageDress", ex);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageDress(ItemViewModel model)
        {
            //PhiUser currentUser = _userStore.FindByNameAsync(User.Identity.GetUserName()).Result;
            if (ModelState.IsValid)
            {
                try
                {
                    string userId = User.Identity.GetUserId();
                    var userItemProviders = this._dataStore.GetItemProvidersByUserProfile(userId).FirstOrDefault();

                    if (userItemProviders == null)
                    {
                        var profile = this._userProfileStore.GetUserProfileById(userId);

                        userItemProviders = DataHelper.TryToInitItemProviderForUser(profile != null ? profile.CompanyName : string.Empty,
                            string.Empty,
                            profile != null ? profile.CompanyEmail : string.Empty,
                            profile != null ? profile.CompanyPhone : string.Empty,
                            (profile != null && profile.LocationId.HasValue) ? profile.LocationId.Value : 1 /* default */,
                            userId,
                            _dataStore);
                    }

                    if (userItemProviders != null)
                    {
                        //base.CurrentLang.Id
                        Item item = new Item();
                        item.Name = model.Name;
                        item.Description = model.Description;
                        item.Gender = model.Gender.Value;
                        item.MadeBy = model.MadeBy;
                        item.ProvideBy = model.ProvideBy;
                        item.SuggestionTerms = model.SuggestionTermsTemperature.Value + model.SuggestionTermsAdditional + model.SuggestionTermsExtra;
                        item.Season = model.Season.Value;
                        item.WaterProtectionPercent = model.WaterProtectionPercent;
                        item.IceProtectionPercent = model.IceProtection;
                        item.ArmoringPercent = model.ArmoringPercent;
                        item.SunProtectionPercent = model.SunProtectionPercent;
                        item.MinAge = model.MinAge;
                        item.MaxAge = model.MaxAge;
                        item.Year = new DateTime(model.Year, 1, 1);
                        item.IsPublic = model.IsPublic;
                        item.ActionTypeId = model.ActionTypeId.Value;
                        item.LanguageId = base.CurrentLang.Id; //todo
                        item.IsWardrobe = true;

                        var context = ModelContainer.Instance.GetInstance<phiContext>();
                        using (var dbContextTransaction = context.Database.BeginTransaction())
                        {
                            try
                            {
                                this._dataStore.Insert(item);

                                // We need an item id to create and link other entities.
                                var pItem = new ProvidersItem
                                {
                                    ItemId = item.Id,
                                    ItemProvidersId = userItemProviders.ItemProviderId
                                };

                                Image image = new Image
                                {
                                    ImageUrl = model.ImageUrl,
                                    ItemId = item.Id,
                                    // Height = 0,
                                    // Width = 0
                                };

                                this._dataStore.Insert(pItem);
                                this._dataStore.Insert(image);

                                dbContextTransaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                _logger.Error("Exception HttpPost ManageDress - Transaction Failed On Insert", ex);

                                dbContextTransaction.Rollback();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error("Exception HttpPost ManageDres", ex);
                }

                return RedirectToAction("DressRoom");
            }

            return View(model);
        }

        public ActionResult Cancel()
        {
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Private methods

        private void _GatherStatistics(string userId, string actionPage)
        {
            try
            {
                this._dataStore.Insert(new UserStat
                {
                    ActionPage = actionPage,
                    Action = "Visit",
                    UserId = userId,
                    Browser = HttpContext.Request.Browser.Browser,
                    DateTime = DateTime.UtcNow,
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