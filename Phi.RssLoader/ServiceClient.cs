/*
 * COPYRIGHT 2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace Phi.RssLoader
{
    public class ServiceClient
    {
        #region Public methods

        public string GetResponse(string requestUrl)
        {
            if (string.IsNullOrEmpty(requestUrl))
            {
                throw new ArgumentNullException("requestUrl");
            }

            return _SendRequest(requestUrl);
        }

        #endregion

        #region Private methods

        private string _SendRequest(string request)
        {
            Debug.Assert(request != null);

            var httpWReq = (HttpWebRequest)WebRequest.Create(request);
            httpWReq.Method = "GET";
            HttpWebResponse httpWRes = null;
            string response;

            try
            {
                httpWRes = (HttpWebResponse)httpWReq.GetResponse();

                if (httpWRes.StatusCode != HttpStatusCode.OK)
                {
                    throw new RssLoaderException("Bad server response: " + httpWRes.StatusCode);
                }

                using (Stream responseStream = httpWRes.GetResponseStream())
                {
                    using (var rd = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        response = rd.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                throw new RssLoaderException("Exception occured during server request: " +
                    ex.Message + Environment.NewLine + ex.StackTrace);
            }
            catch (Exception ex)
            {
                throw new RssLoaderException("Exception occured during server request: " +
                    ex.Message + Environment.NewLine + ex.StackTrace);
            }
            finally
            {
                if (httpWRes != null)
                {
                    httpWRes.Close();
                }
            }

            return response;
        }

        #endregion
    }
}
