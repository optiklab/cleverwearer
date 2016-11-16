/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.MobileWebApp.Controllers
{
    using System.Globalization;
    using System.Threading;
    using System.Web.Mvc;
    using Phi.Models.Models;
    using Phi.Repository;
    using Phi.Repository.Stores;

    public class BaseController : Controller
    {
        public string CurrentLangCode { get; protected set; }

        public Language CurrentLang { get; protected set; }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            //if (requestContext.HttpContext.Request.Url != null)
            //{
            //    HostName = requestContext.HttpContext.Request.Url.Authority;
            //}

            if (requestContext.RouteData.Values["lang"] != null && requestContext.RouteData.Values["lang"] as string != "null")
            {
                CurrentLangCode = requestContext.RouteData.Values["lang"] as string;

                IDataStore dataStore = ModelContainer.Instance.GetInstance<IDataStore>();
                CurrentLang = dataStore.GetLanguageByCode(CurrentLangCode);

                var ci = new CultureInfo(CurrentLangCode);
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            }
            base.Initialize(requestContext);
        }
    }
}