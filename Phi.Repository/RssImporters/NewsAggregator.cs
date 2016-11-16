/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

using System;
using System.Collections.Generic;
using Phi.Repository.External;
using Phi.RssLoader;

namespace Phi.Repository.RssImporters
{
    public class NewsAggregator
    {
        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Dictionary<string, List<News>> GetNews(List<NewsProvider> providers)
        {
            var result = new Dictionary<string, List<News>>();

            foreach (NewsProvider provider in providers)
            {
                RssResponse response = null;
                try
                {
                    var rssLoader = new RssLoader.RssLoader();

                    response = rssLoader.Load(provider.Url);
                }
                catch (Exception ex)
                {
                    _logger.Error(string.Format("Failed to get info from {0}:", provider.ProviderName), ex);
                }

                try
                {
                    if (response != null && response.Channel != null)
                    {
                        var news = new List<News>();

                        foreach (var item in response.Channel.Item)
                        {
                            var newsItem = new News
                            {
                                Header = item.Title != null ? item.Title.Trim() : item.Title,
                                Description = item.Description != null ? item.Description.Trim() : item.Description,
                                Tags = BuildTags(provider.Type.ToString().ToLower(), item.Category)
                            };

                            DateTime publishDate;
                            if (DateTime.TryParse(item.PubDate, out publishDate))
                            {
                                newsItem.PublishDateTime = publishDate;
                            }
                            else
                            {
                                newsItem.PublishDateTime = DateTime.UtcNow;
                            }

                            if (!string.IsNullOrEmpty(item.Link) &&
                                Uri.IsWellFormedUriString(item.Link, UriKind.Absolute))
                            {
                                newsItem.SourceLink = item.Link;
                            }
                            else if (!string.IsNullOrEmpty(item.Guid) &&
                                     Uri.IsWellFormedUriString(item.Guid, UriKind.Absolute))
                            {
                                newsItem.SourceLink = item.Guid;
                            }
                            else
                            {
                                newsItem.SourceLink = provider.Url;
                            }

                            news.Add(newsItem);
                        }

                        result.Add(provider.ProviderName, news);
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(string.Format("Failed to save info from {0}:", provider.ProviderName), ex);
                }
            }

            return result;
        }

        private string BuildTags(string type, List<string> categories)
        {
            string tags = string.Empty;

            if (categories != null)
            {
                foreach (string cat in categories)
                {
                    tags += ", " + cat;
                }

                tags = tags.Trim().TrimStart(',').TrimEnd(',').Trim().ToLower();
            }

            if (string.IsNullOrEmpty(tags))
            {
                return type;
            }

            return string.Format(@"{0}, {1}", type, tags).Trim().TrimEnd(',').Trim();
        }
    }
}
