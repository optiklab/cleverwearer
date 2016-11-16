/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

using System.Linq;
using Phi.Models.Models;
using Phi.Repository.RssImporters;

namespace Phi.MobileWebApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using Repository;
    using Repository.Stores;
    using Repository.External;

    [AllowAnonymous]
    public class FeedController : BaseApiController
    {
        #region Private fields

        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IDataStore _dataStore;
        private readonly NewsAggregator _newsAggregator;

        #endregion

        #region Public constructor

        public FeedController(IDataStore dataStore)
        {
            _dataStore = dataStore;
            _newsAggregator = new NewsAggregator();
        }

        #endregion

        #region Public methods

        // /ru/api/feed/update
        [HttpGet]
        [ActionName("update")]
        public void UpdateNews()
        {
            try
            {
                var providers = new List<NewsProvider>
                {
                    // Fashion sites.
                    new NewsProvider(@"riamoda.ru", 2, NewsConstants.UpdateHours, @"http://riamoda.ru/rss.xml", NewsProviderType.Fashion),
                    new NewsProvider(@"Modanews.ru", 2, NewsConstants.UpdateHours, @"http://modanews.ru/rss/news/", NewsProviderType.Fashion),
                    new NewsProvider(@"glianec.com.u", 2, NewsConstants.UpdateHours, @"http://glianec.com.ua/?format=feed&type=rss", NewsProviderType.Fashion),
                    new NewsProvider(@"fashiontime.ru/main", 2, NewsConstants.UpdateHours, @"http://www.fashiontime.ru/rss/content.xml", NewsProviderType.Fashion),
                    new NewsProvider(@"fashiontime.ru/news", 2, NewsConstants.UpdateHours, @"http://www.fashiontime.ru/rss/content_news.xml", NewsProviderType.Fashion),
                    new NewsProvider(@"fashiontime.ru/articles", 2, NewsConstants.UpdateHours, @"http://www.fashiontime.ru/rss/content_article.xml", NewsProviderType.Fashion),
                    new NewsProvider(@"intermoda.ru", 2, NewsConstants.UpdateHours, @"http://www.intermoda.ru/rss/news", NewsProviderType.Fashion),
                    new NewsProvider(@"profashion.ru", 2, NewsConstants.UpdateHours, @"http://profashion.ru/news/rss/", NewsProviderType.Fashion),
                    new NewsProvider(@"fashionunited.ru", 2, NewsConstants.UpdateHours, @"https://fashionunited.ru/rss-novosti", NewsProviderType.Fashion),
                    new NewsProvider(@"Style.rbc.ru", 2, NewsConstants.UpdateHours, @"http://static.feed.rbc.ru/rbc/internal/rss.rbc.ru/style.rbc.ru/popural.rss", NewsProviderType.Fashion),
                    new NewsProvider(@"be-in.ru", 2, NewsConstants.UpdateHours, @"http://www.be-in.ru/rss/moda", NewsProviderType.Fashion),
                    new NewsProvider(@"dress-code.com.ua", 2, NewsConstants.UpdateHours, @"http://www.dress-code.com.ua/rss/common.rss", NewsProviderType.Fashion),
                    new NewsProvider(@"trendymen.ru", 2, NewsConstants.UpdateHours, @"http://trendymen.ru/_services/rss.php", NewsProviderType.Fashion),
                    new NewsProvider(@"fashionpeople.ru", 2, NewsConstants.UpdateHours, @"http://www.fashionpeople.ru/xml/rss.xml", NewsProviderType.Fashion),

                    // Tech sites
                    new NewsProvider(@"3dnews.ru", 2, NewsConstants.UpdateHours, @"http://www.3dnews.ru/news/rss"),
                    //new NewsProvider(@"popmech.ru", 2, NewsConstants.UpdateHours, @"http://www.popmech.ru/rss"),
                    new NewsProvider(@"mobbit", 2, NewsConstants.UpdateHours, @"http://mobbit.info/rss.php"),
                    new NewsProvider(@"ixbt.co", 2, NewsConstants.UpdateHours, @"http://www.ixbt.com/export/articles.rdf"),
                    new NewsProvider(@"mobile-review", 2, NewsConstants.UpdateHours, @"http://www.mobile-review.com/newslistouter/rssnewsfull.xml"),
                    new NewsProvider(@"mobile-review-reviews", 2, NewsConstants.UpdateHours, @"http://www.mobile-review.com/rss-review.xml"),
                    new NewsProvider(@"tech", 2, NewsConstants.UpdateHours, @"https://www.google.com/news?output=rss&topic=t&ned=ru_ru&cf=all&num=20"),
                    new NewsProvider(@"astronet.ru", 2, NewsConstants.UpdateHours, @"http://www.astronet.ru/db/rss.xml"),
                    new NewsProvider(@"nkj.ru", 2, NewsConstants.UpdateHours, @"http://www.nkj.ru/rss/iblock_rss_31.xml"),
                    new NewsProvider(@"elementy.r", 2, NewsConstants.UpdateHours, @"http://elementy.ru/rss/news"),
                    new NewsProvider(@"postnauka.ru", 2, NewsConstants.UpdateHours, @"http://postnauka.ru/feed"),
                    new NewsProvider(@"habrahabr-startups", 2, NewsConstants.UpdateHours, @"http://habrahabr.ru/rss/tag/%D1%81%D1%82%D0%B0%D1%80%D1%82%D0%B0%D0%BF%D1%8B"), // стартапы
                    new NewsProvider(@"habrahabr", 2, NewsConstants.UpdateHours, @"http://www.habrahabr.ru/rss/main/"), // feed/http://habrahabr.ru/rss/best/
                    new NewsProvider(@"geektimes.ru", 2, NewsConstants.UpdateHours, @"http://geektimes.ru/rss/all/"), // http://geektimes.ru/rss/best/
                    //new NewsProvider(@"", 2, NewsConstants.UpdateHours, @""), 

                    new NewsProvider(@"The Verge", 1, NewsConstants.UpdateHours, @"http://www.theverge.com/rss/full.xml"),
                    new NewsProvider(@"Engadget", 1, NewsConstants.UpdateHours, @"http://www.engadget.com/rss-full.xml"),
                    new NewsProvider(@"Lifehacker", 1, NewsConstants.UpdateHours, @"http://feeds.gawker.com/lifehacker/vip"), // Lifehacker
                    //new NewsProvider(@"readwriteweb", 1, NewsConstants.UpdateHours, @"http://www.readwriteweb.com/rss.xml"),
                    new NewsProvider(@"Techcrunch", 1, NewsConstants.UpdateHours, @"http://feeds.feedburner.com/Techcrunch"), //Techcrunch
                    new NewsProvider(@"Wired", 1, NewsConstants.UpdateHours, @"http://feeds.wired.com/wired/index"),
                    new NewsProvider(@"Gizmodo", 1, NewsConstants.UpdateHours, @"http://feeds.gawker.com/gizmodo/full"), // Gizmodo
                    new NewsProvider(@"Mashable", 1, NewsConstants.UpdateHours, @"http://feeds.mashable.com/Mashable"),
                    new NewsProvider(@"arstechnica", 1, NewsConstants.UpdateHours, @"http://feeds.arstechnica.com/arstechnica/index/"),
                    new NewsProvider(@"CNET", 1, NewsConstants.UpdateHours, @"http://news.com.com/2547-1_3-0-5.xml"),
                    new NewsProvider(@"Slashdot", 1, NewsConstants.UpdateHours, @"http://rss.slashdot.org/Slashdot/slashdot"),
                    new NewsProvider(@"Google Blog", 1, NewsConstants.UpdateHours, @"http://googleblog.blogspot.com/atom.xml"),
                    new NewsProvider(@"The Next Web", 1, NewsConstants.UpdateHours, @"http://feeds2.feedburner.com/thenextweb"),
                    new NewsProvider(@"BoingBoing", 1, NewsConstants.UpdateHours, @"http://boingboing.net/feed"),
                    new NewsProvider(@"ThinkGeek", 1, NewsConstants.UpdateHours, @"http://www.thinkgeek.com/thinkgeek.rss"),
                    new NewsProvider(@"TED Talks", 1, NewsConstants.UpdateHours, @"http://feeds.feedburner.com/tedtalks_video"),
                    new NewsProvider(@"YCombinator", 1, NewsConstants.UpdateHours, @"https://news.ycombinator.com/rss"),
                    //new NewsProvider(@"", 1, NewsConstants.UpdateHours, @""),
                };

                if (_dataStore.IsNeedToUpdate(NewsConstants.UpdateHours))
                {
                    Dictionary<string, List<News>> news = _newsAggregator.GetNews(providers);

                    if (news != null)
                    {
                        foreach (string providerName in news.Keys)
                        {
                            NewsProvider providerInfo = providers.FirstOrDefault(x => x.ProviderName == providerName);

                            foreach (var item in news[providerName])
                            {
                                if (IsNewsAcceptable(item, providerInfo))
                                {
                                    item.ThemeOrCategory = DetectTheme(item);

                                    InsertNewsItem(
                                        ModelAdapter.ConvertNewsToBlog(item, providerInfo));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Exception UpdateNews", ex);
            }
        }

        #endregion

        #region Private methods
        
        private bool IsNewsAcceptable(News newsItem, NewsProvider providerInfo)
        {
            foreach (var token in GetTokens(providerInfo.Type, providerInfo.LanguageId))
            {
                if ((newsItem.Header != null && newsItem.Header.IndexOf(token, StringComparison.InvariantCultureIgnoreCase) > -1) ||
                    (newsItem.Description != null && newsItem.Description.IndexOf(token, StringComparison.InvariantCultureIgnoreCase) > -1))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns tokens which helps to differentiate medium Tech news from Smart Clothes news.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        private string[] GetTokens(NewsProviderType type, int language)
        {
            if (type == NewsProviderType.Tech)
            {
                if (language == 1)
                {
                    return new[] { "clothe", "clothi", "wears", "weari", "smart-", "IOT" };
                }

                return new[] { "одежд", "текстиль", "смарт-" };
            }
            else
            {
                if (language == 1)
                {
                    return new[] { "smart-", "3d-print" };
                }

                return new[] { "смарт", "умных", "умные", "умная", "смарт-", "3d-печат", "3d-принт" };
            }
        }

        /// <summary>
        /// Detects main theme of a news.
        /// </summary>
        /// <param name="newsItem"></param>
        /// <param name="languageId"></param>
        /// <returns></returns>
        private string DetectTheme(News newsItem)
        {
            var possible = new Dictionary<NewsTags, string[]>()
            {
                {
                    NewsTags.Devices, new [] { "часы", "часов", "watch", "наушники", "phones", "audio", "звук", "аудио", "фитнес", "трекер", "fitnes", "track", "cars", "автомобил", "автономн", "autonomo" }
                },
                {
                    NewsTags.AI, new [] { "artificial", "intelligence", "machine learning", "recognition", "deep learning", "интеллект", "машинно" }
                },
                {
                    NewsTags.Robots, new [] { "robot", "робот" }
                },
                // Important to have it last.
                {
                    NewsTags.Clothes, new [] { "clothe", "clothi", "wears", "weari", "smart-", "одежд", "текстиль", "смарт-", "умных", "умные", "умная" }
                },
                {
                    NewsTags.Startups, new [] { "startup", "стартап" }
                }
            };

            foreach (NewsTags tag in possible.Keys)
            {

                for (int i = 0; i < possible[tag].Length; i++)
                {
                    if ((newsItem.Header != null && newsItem.Header.IndexOf(possible[tag][i], StringComparison.InvariantCultureIgnoreCase) > -1) ||
                        (newsItem.Description != null && newsItem.Description.IndexOf(possible[tag][i], StringComparison.InvariantCultureIgnoreCase) > -1))
                    {
                        return NewsTagsHelpers.GetTagDescription(tag);
                    }
                }
            }

            return NewsTagsHelpers.GetTagDescription(NewsTags.Common);
        }

        private void InsertNewsItem(Blog newsItem)
        {
            if (newsItem != null && !_dataStore.GetBlogs(newsItem.ProviderName, newsItem.Header).Any())
            {
                _dataStore.Insert(newsItem);
            }
        }

        #endregion
    }
}
