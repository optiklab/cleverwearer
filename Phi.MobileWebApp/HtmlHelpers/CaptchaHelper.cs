/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.MobileWebApp.HtmlHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    /// <summary>
    /// Extension class for HtmlHelper.
    /// </summary>
    public static class CaptchaHelper
    {
        #region Public methods

        /// <summary>
        ///  Generates captcha image with unique value and guid to validate requests.
        /// </summary>
        public static MvcHtmlString Captcha(this HtmlHelper html, string name)
        {
            string challengeGuid = Guid.NewGuid().ToString();

            // Generate and save unique text for captcha.
            var session = html.ViewContext.HttpContext.Session;
            session[SESSION_KEY_PREFIX + challengeGuid] = _MakeRandomSolution();

            // Generate page with captcha image and hidder field containing unique Guid of the captcha.
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            string url = urlHelper.Action("Render", "CaptchaImage", new { challengeGuid });
            return MvcHtmlString.Create(string.Format(IMAGE_FORMAT, url) + html.Hidden(name, challengeGuid));
        }

        /// <summary>
        /// Verifies user input.
        /// </summary>
        public static bool VerifyAndExpireSolution(HttpContextBase context, string challengeGuid, string attemptedSolution)
        {
            string solution = (string)context.Session[SESSION_KEY_PREFIX + challengeGuid];

            context.Session.Remove(SESSION_KEY_PREFIX + challengeGuid);

            return ((solution != null) && (attemptedSolution == solution));
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Generates random phrase.
        /// </summary>
        private static string _MakeRandomSolution()
        {
            Random rng = new Random();

            int len = rng.Next(5, 7);

            char[] buf = new char[len];

            for (int i = 0; i < len; i++)
                buf[i] = (char)('a' + rng.Next(26));

            return new string(buf);
        }

        #endregion

        #region Constants

        internal const string SESSION_KEY_PREFIX = "__captcha";
        private const string IMAGE_FORMAT = "<img src=\"{0}\" />";

        #endregion
    }
}