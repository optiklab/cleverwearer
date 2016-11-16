/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.Repository.Helpers
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Web;
    using System.Web.Hosting;

    /// <summary>
    /// Represents a common helper
    /// </summary>
    public class WebHelper : IWebHelper
    {
        #region Private fields

        private readonly HttpContextBase _httpContext;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpContext">HTTP context</param>
        public WebHelper(HttpContextBase httpContext)
        {
            this._httpContext = httpContext;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Get URL referrer
        /// </summary>
        /// <returns>URL referrer</returns>
        public string GetUrlReferrer()
        {
            string referrerUrl = string.Empty;

            if (this._httpContext != null &&
                this._httpContext.Request != null &&
                this._httpContext.Request.UrlReferrer != null)
            {
                referrerUrl = this._httpContext.Request.UrlReferrer.ToString();
            }

            return referrerUrl;
        }

        /// <summary>
        /// Get context IP address
        /// </summary>
        /// <returns>URL referrer</returns>
        public string GetCurrentIpAddress()
        {
            if (this._httpContext != null && this._httpContext.Request != null && this._httpContext.Request.UserHostAddress != null)
            {
                return this._httpContext.Request.UserHostAddress;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets a value indicating whether current connection is secured
        /// </summary>
        /// <returns>true - secured, false - not secured</returns>
        public Boolean IsCurrentConnectionSecured()
        {
            Boolean useSsl = false;
            if (this._httpContext != null && this._httpContext.Request != null)
            {
                useSsl = this._httpContext.Request.IsSecureConnection;

                // when hosting uses a load balancer on their server then the Request.IsSecureConnection is never got set to true, use the statement below
                // just uncomment it
                // useSSL = _httpContext.Request.ServerVariables["HTTP_CLUSTER_HTTPS"] == "on" ? true : false;
            }

            return useSsl;
        }

        /// <summary>
        /// Gets a value indicating whether connection should be secured
        /// </summary>
        /// <returns>Result</returns>
        public Boolean SslEnabled()
        {
            return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["UseSSL"]) &&
                Convert.ToBoolean(ConfigurationManager.AppSettings["UseSSL"]);
        }

        /// <summary>
        /// Gets store location
        /// </summary>
        /// <returns>Store location</returns>
        public string GetStoreLocation()
        {
            string result = this.GetStoreHost();

            if (result.EndsWith("/"))
            {
                result = result.TrimEnd('/'); // result = result.Substring(0, result.Length - 1);
            }

            if (this._httpContext != null && this._httpContext.Request != null)
            {
                result = result + this._httpContext.Request.ApplicationPath;
            }

            if (!result.EndsWith("/"))
            {
                result += "/";
            }

            return result.ToLowerInvariant();
        }

        /// <summary>
        /// Gets store host location
        /// </summary>
        /// <returns>Store host location</returns>
        public string GetStoreHost()
        {
            string result = "http://" + this.ServerVariables("HTTP_HOST");
            if (!result.EndsWith("/"))
            {
                result += "/";
            }

            return result.ToLowerInvariant();
        }

        /// <summary>
        /// Gets server variable by name
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Server variable</returns>
        public string ServerVariables(string name)
        {
            string tmpS = string.Empty;
            try
            {
                if (this._httpContext.Request.ServerVariables[name] != null)
                {
                    tmpS = this._httpContext.Request.ServerVariables[name];
                }
            }
            catch
            {
                tmpS = string.Empty;
            }
            return tmpS;
        }

        /// <summary>
        /// Returns true if the requested resource is one of the typical resources that needn't be processed by the cms engine.
        /// </summary>
        /// <param name="request">HTTP Request</param>
        /// <returns>True if the request targets a static resource file.</returns>
        public Boolean IsStaticResource(HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            string path = request.Path;
            string extension = VirtualPathUtility.GetExtension(path);

            if (extension == null)
            {
                return false;
            }

            switch (extension.ToLower())
            {
                case ".axd":
                case ".ashx":
                case ".bmp":
                case ".css":
                case ".gif":
                case ".ico":
                case ".jpeg":
                case ".jpg":
                case ".js":
                case ".png":
                case ".rar":
                case ".zip":
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Maps a virtual path to a physical disk path.
        /// </summary>
        /// <param name="path">The path to map. E.g. "~/bin"</param>
        /// <returns>The physical path. E.g. "c:\inetpub\wwwroot\bin"</returns>
        public string MapPath(string path)
        {
            if (HostingEnvironment.IsHosted)
            {
                // hosted
                return HostingEnvironment.MapPath(path);
            }

            // not hosted. For example, run in unit tests
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Replace("~/", String.Empty).TrimStart('/').Replace('/', '\\');
            return Path.Combine(baseDirectory, path);
        }

        /// <summary>
        /// Get a value indicating whether the request is made by search engine (web crawler)
        /// </summary>
        /// <param name="request">HTTP Request</param>
        /// <returns>Result</returns>
        public Boolean IsSearchEngine(HttpRequestBase request)
        {
            if (request == null)
            {
                return false;
            }

            Boolean result = false;
            try
            {
                result = request.Browser.Crawler;
                if (!result)
                {
                    // TODO
                    // put any additional known crawlers in the Regex below for some custom validation
                    // var regEx = new Regex("Twiceler|twiceler|BaiDuSpider|baduspider|Slurp|slurp|ask|Ask|Teoma|teoma|Yahoo|yahoo");
                    // result = regEx.Match(request.UserAgent).Success;
                }
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
            }
            return result;
        }

        #endregion

        #region Private methods

        private Boolean CanWriteToWebConfig()
        {
            try
            {
                File.SetLastWriteTimeUtc(this.MapPath("~/web.config"), DateTime.UtcNow);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private Boolean CanWriteToGlobalAsax()
        {
            try
            {
                File.SetLastWriteTimeUtc(this.MapPath("~/global.asax"), DateTime.UtcNow);

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
