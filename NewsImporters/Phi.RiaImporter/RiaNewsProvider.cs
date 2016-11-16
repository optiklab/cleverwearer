using Phi.Models;
using Phi.Models.Models;
using Phi.Repository;
using Phi.Repository.Enums;
using Phi.Repository.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Phi.RiaImporter
{
    /// <summary>
    /// 
    /// </summary>
    public class RiaNewsProvider : INewsProvider
    {
        #region Public methods

        public string ProviderName { get { return @"http://riamoda.ru"; } }
        public int LanguageId { get { return 2; } }
        public int UpdatePeriodHours { get { return 24; } }

        public List<Phi.Repository.External.News> GetNews()
        {
            var news = new List<Repository.External.News>();
            try
            {
                ServiceClient client = new ServiceClient();
                string wResponse = client.GetResponse(@"http://riamoda.ru/rss.xml");

                var result = XmlHelpers.ConvertStringToXMLObject(typeof(RssRiaResponse), wResponse) as RssRiaResponse;

                if (result != null && result.Channel != null)
                {
                    foreach (var item in result.Channel.Item)
                    {
                        var newsItem = new Phi.Repository.External.News
                        {
                            Header = item.Title,
                            ThemeOrCategory = ProviderName,
                            Description = item.Description,
                            SourceLink = item.Guid
                        };

                        DateTime publishDate;
                        if (DateTime.TryParse(item.PubDate, out publishDate))
                        {
                            newsItem.PublishDateTime = publishDate; // To UTC
                        }

                        news.Add(newsItem);
                    }
                }
            }
            catch (Exception)
            {
            }

            return news;
        }
        
        #endregion
    }
}
