/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.MobileWebApp
{
    using System.Web;
    using System.Web.Mvc;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            filters.Add(new System.Web.Mvc.AuthorizeAttribute());

            // TODO AY HTTPS.
            //filters.Add(new RequireHttpsAttribute());
        }
    }
}
