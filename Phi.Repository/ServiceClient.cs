/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

using Phi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Phi.Repository
{
    /// <summary>
    /// Клиент, выполняющий запросы на поставщика.
    /// </summary>
    public class ServiceClient
    {
        #region Public methods

        /// <summary>
        /// Возвращает XML ответ от провайдера.
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <returns>XML ответ.</returns>
        public string GetResponse(string requestUrl)
        {
            if (string.IsNullOrEmpty(requestUrl))
            {
                throw new ArgumentNullException("requestUrl");
            }

            return _SendRequest(requestUrl);
        }

        public string GetHtmlContentResponse(string requestUrl)
        {
            if (string.IsNullOrEmpty(requestUrl))
            {
                throw new ArgumentNullException("requestUrl");
            }

            return _SendRequest(requestUrl);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Отправляет запрос.
        /// </summary>
        /// <returns>Ответ</returns>
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
                    throw new RejectedByProviderException("Bad server response: " + httpWRes.StatusCode);
                }

                // Загрузка файла
                using (Stream responseStream = httpWRes.GetResponseStream())
                {
                    using (var rd = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        response = rd.ReadToEnd();
                    }

                    //responseStream.Close();
                }
            }
            catch (WebException ex)
            {
                throw new RejectedByProviderException("Exception occured during server request: " +
                    ex.Message + Environment.NewLine + ex.StackTrace);
            }
            catch (Exception ex)
            {
                throw new RejectedByProviderException("Exception occured during server request: " +
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
