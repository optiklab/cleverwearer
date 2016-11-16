/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.Repository.Helpers
{
    using System;
    using System.Web;

    /// <summary>
    /// Represents a common helper
    /// </summary>
    public interface IWebHelper
    {
        /// <summary>
        /// Get URL referrer
        /// </summary>
        /// <returns>URL referrer</returns>
        string GetUrlReferrer();

        /// <summary>
        /// Get context IP address
        /// </summary>
        /// <returns>URL referrer</returns>
        string GetCurrentIpAddress();

        /// <summary>
        /// Gets a value indicating whether current connection is secured
        /// </summary>
        /// <returns>true - secured, false - not secured</returns>
        Boolean IsCurrentConnectionSecured();

        /// <summary>
        /// Gets a value indicating whether connection should be secured
        /// </summary>
        /// <returns>Result</returns>
        Boolean SslEnabled();

        /// <summary>
        /// Gets store location
        /// </summary>
        /// <returns>Store location</returns>
        string GetStoreLocation();

        /// <summary>
        /// Gets store host location
        /// </summary>
        /// <returns>Store host location</returns>
        string GetStoreHost();

        /// <summary>
        /// Gets server variable by name
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Server variable</returns>
        string ServerVariables(string name);

        /// <summary>
        /// Returns true if the requested resource is one of the typical resources that needn't be processed by the cms engine.
        /// </summary>
        /// <param name="request">HTTP Request</param>
        /// <returns>True if the request targets a static resource file.</returns>
        Boolean IsStaticResource(HttpRequest request);

        /// <summary>
        /// Maps a virtual path to a physical disk path.
        /// </summary>
        /// <param name="path">The path to map. E.g. "~/bin"</param>
        /// <returns>The physical path. E.g. "c:\inetpub\wwwroot\bin"</returns>
        string MapPath(string path);

        /// <summary>
        /// Get a value indicating whether the request is made by search engine (web crawler)
        /// </summary>
        /// <param name="request">HTTP Request</param>
        /// <returns>Result</returns>
        Boolean IsSearchEngine(HttpRequestBase request);
    }
}
