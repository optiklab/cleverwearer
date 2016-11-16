using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phi.MobileWebApp.Controllers
{
    [Authorize]
    public class WishListController : Controller
    {
        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: WishList
        public ActionResult WishList()
        {
            return View();
        }
    }
}