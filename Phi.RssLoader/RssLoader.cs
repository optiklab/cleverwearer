/*
 * COPYRIGHT 2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/
using System;

namespace Phi.RssLoader
{
    /// <summary>
    /// 
    /// </summary>
    public class RssLoader
    {
        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        #region Public methods

        public RssResponse Load(string url)
        {
            RssResponse result = null;
            string wResponse = null;
            try
            {
                ServiceClient client = new ServiceClient();
                wResponse = client.GetResponse(url);
            }
            catch (Exception)
            {
            }

            if (!string.IsNullOrEmpty(wResponse))
            {
#if DEBUG
                _logger.Info(string.Format("RSS RESPONSE from {0}: {1}", url, wResponse));
#endif
                if (!TryConvert(wResponse, ref result))
                {
                    TryConvert(RemoveEcnlosures(wResponse), ref result);
                }
            }

            return result;
        }

        private bool TryConvert(string webResponse, ref RssResponse rssResponse)
        {
            bool result = true;

            try
            {
                rssResponse = XmlHelpers.ConvertStringToXMLObject(typeof(RssResponse), webResponse) as RssResponse;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        private string RemoveEcnlosures(string webResponse)
        {
            int index = webResponse.IndexOf("<enclosure");
            int endIndex = webResponse.IndexOf("/>", index + 1);

            while (index > 0 && endIndex > 0)
            {
                webResponse = webResponse.Remove(index, endIndex - index + 2);

                index = webResponse.IndexOf("<enclosure");
                endIndex = webResponse.IndexOf("/>", index + 1);
            }

            return webResponse;
        }
        
        #endregion
    }
}
