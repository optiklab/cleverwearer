using System;
using System.Web.Mvc;
using Phi.MobileWebApp.Models;
using Phi.Models.Models;
using Phi.Repository;
using Phi.Repository.Stores;

namespace Phi.MobileWebApp.Controllers
{
    [AllowAnonymous]
    public class ToolsController : BaseController
    {
        #region Private fields

        private const string INSTAGRAM_TOKEN = @"_ovg3g"" data-reactid=""";

        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IDataStore _dataStore;
        #endregion

        #region Public constructor

        public ToolsController(IDataStore dataService)
        {
            this._dataStore = dataService;
        }

        #endregion

        // GET: Tools
        [HttpGet]
        public ActionResult SaveInstagram()
        {
            _GatherStatistics(null, null, null, "SaveInstagram");

            var model = new ToolsViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveInstagram(ToolsViewModel model)
        {
            string url = string.Empty;

            if (ModelState.IsValid)
            {
                model.Url = model.Path;
            }

            return View(model);
        }

        // POST: Tools
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult SaveInstagram(ToolsViewModel model)
        //{
        //    string url = string.Empty;

        //    if (ModelState.IsValid)
        //    {
        //        ServiceClient client = new ServiceClient();
        //        string wResponse = client.GetHtmlContentResponse(model.Path);

        //        int index = wResponse.LastIndexOf(INSTAGRAM_TOKEN, 0);

        //        if (index > 0)
        //        {
        //            int lastIndex = wResponse.LastIndexOf(@"""", index + INSTAGRAM_TOKEN.Length);

        //            url = wResponse.Substring(index + INSTAGRAM_TOKEN.Length, lastIndex - index - INSTAGRAM_TOKEN.Length);
        //        }
        //    }

        //    return Redirect(url); //RedirectToAction("SavedInstagram");
        //}

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
    }
}