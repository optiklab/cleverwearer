using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Phi.Repository.External;
using Phi.Repository.RssImporters;
using Phi.Repository;

namespace Phi.ImportersTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestNewsImporter();
            TestWeatherProvider();

            Console.WriteLine("Press any key to finish...");

            Console.ReadKey();
        }

        private static void TestWeatherProvider()
        {
            IDataProvider provider = new OpenWeatherMapProvider.DataProvider();
            var collection = provider.GetWeatherForecastByWoeid("2643743", "metric");
        }

        private static void TestNewsImporter()
        {
            var providers = new List<NewsProvider>
                {
                    // Fashion sites.
                    new NewsProvider(@"riamoda.ru", 2, 24, @"http://riamoda.ru/rss.xml", NewsProviderType.Fashion),
                    new NewsProvider(@"Modanews.ru", 2, 24, @"http://modanews.ru/rss/news/", NewsProviderType.Fashion),
                    new NewsProvider(@"glianec.com.u", 2, 24, @"http://glianec.com.ua/?format=feed&type=rss", NewsProviderType.Fashion),
                    new NewsProvider(@"fashiontime.ru - публикации", 2, 24, @"http://www.fashiontime.ru/rss/content.xml", NewsProviderType.Fashion),
                    new NewsProvider(@"fashiontime.ru - новости", 2, 24, @"http://www.fashiontime.ru/rss/content_news.xml", NewsProviderType.Fashion),
                    new NewsProvider(@"fashiontime.ru - статьи", 2, 24, @"http://www.fashiontime.ru/rss/content_article.xml", NewsProviderType.Fashion),
                    new NewsProvider(@"intermoda.ru", 2, 24, @"http://www.intermoda.ru/rss/news", NewsProviderType.Fashion),
                    new NewsProvider(@"profashion.ru", 2, 24, @"http://profashion.ru/news/rss/", NewsProviderType.Fashion),
                    new NewsProvider(@"fashionunited.ru", 2, 24, @"https://fashionunited.ru/rss-novosti", NewsProviderType.Fashion),
                    new NewsProvider(@"Style.rbc.ru", 2, 24, @"http://static.feed.rbc.ru/rbc/internal/rss.rbc.ru/style.rbc.ru/popural.rss", NewsProviderType.Fashion),
                    new NewsProvider(@"be-in.ru", 2, 24, @"http://www.be-in.ru/rss/moda", NewsProviderType.Fashion),
                    new NewsProvider(@"dress-code.com.ua", 2, 24, @"http://www.dress-code.com.ua/rss/common.rss", NewsProviderType.Fashion),
                    new NewsProvider(@"trendymen.ru", 2, 24, @"http://trendymen.ru/_services/rss.php", NewsProviderType.Fashion),
                    new NewsProvider(@"fashionpeople.ru", 2, 24, @"http://www.fashionpeople.ru/xml/rss.xml", NewsProviderType.Fashion),

                    // Tech sites
                    new NewsProvider(@"3dnews.ru", 2, 24, @"http://www.3dnews.ru/news/rss"),
                    //new NewsProvider(@"popmech.ru", 2, 24, @"http://www.popmech.ru/rss"),
                    new NewsProvider(@"mobbit", 2, 24, @"http://mobbit.info/rss.php"),
                    new NewsProvider(@"ixbt.co", 2, 24, @"http://www.ixbt.com/export/articles.rdf"),
                    new NewsProvider(@"mobile-review", 2, 24, @"http://www.mobile-review.com/newslistouter/rssnewsfull.xml"),
                    new NewsProvider(@"mobile-review-reviews", 2, 24, @"http://www.mobile-review.com/rss-review.xml"),
                    new NewsProvider(@"tech", 2, 24, @"https://www.google.com/news?output=rss&topic=t&ned=ru_ru&cf=all&num=20"),
                    new NewsProvider(@"astronet.ru", 2, 24, @"http://www.astronet.ru/db/rss.xml"),
                    new NewsProvider(@"nkj.ru", 2, 24, @"http://www.nkj.ru/rss/iblock_rss_31.xml"),
                    new NewsProvider(@"elementy.r", 2, 24, @"http://elementy.ru/rss/news"),
                    new NewsProvider(@"postnauka.ru", 2, 24, @"http://postnauka.ru/feed"),
                    new NewsProvider(@"habrahabr-startups", 2, 24, @"http://habrahabr.ru/rss/tag/%D1%81%D1%82%D0%B0%D1%80%D1%82%D0%B0%D0%BF%D1%8B"), // стартапы
                    new NewsProvider(@"habrahabr", 2, 24, @"http://www.habrahabr.ru/rss/main/"), // feed/http://habrahabr.ru/rss/best/
                    new NewsProvider(@"geektimes.ru", 2, 24, @"http://geektimes.ru/rss/all/"), // http://geektimes.ru/rss/best/
                    //new NewsProvider(@"", 2, 24, @""), 

                    new NewsProvider(@"The Verge", 1, 24, @"http://www.theverge.com/rss/full.xml"),
                    new NewsProvider(@"Engadget", 1, 24, @"http://www.engadget.com/rss-full.xml"),
                    new NewsProvider(@"Lifehacker", 1, 24, @"http://feeds.gawker.com/lifehacker/vip"), // Lifehacker
                    //new NewsProvider(@"readwriteweb", 1, 24, @"http://www.readwriteweb.com/rss.xml"),
                    new NewsProvider(@"Techcrunch", 1, 24, @"http://feeds.feedburner.com/Techcrunch"), //Techcrunch
                    new NewsProvider(@"Wired", 1, 24, @"http://feeds.wired.com/wired/index"),
                    new NewsProvider(@"Gizmodo", 1, 24, @"http://feeds.gawker.com/gizmodo/full"), // Gizmodo
                    new NewsProvider(@"Mashable", 1, 24, @"http://feeds.mashable.com/Mashable"),
                    new NewsProvider(@"arstechnica", 1, 24, @"http://feeds.arstechnica.com/arstechnica/index/"),
                    new NewsProvider(@"CNET", 1, 24, @"http://news.com.com/2547-1_3-0-5.xml"),
                    new NewsProvider(@"Slashdot", 1, 24, @"http://rss.slashdot.org/Slashdot/slashdot"),
                    new NewsProvider(@"Google Blog", 1, 24, @"http://googleblog.blogspot.com/atom.xml"),
                    new NewsProvider(@"The Next Web", 1, 24, @"http://feeds2.feedburner.com/thenextweb"),
                    new NewsProvider(@"boingboing", 1, 24, @"http://boingboing.net/feed"),
                    new NewsProvider(@"thinkgeek", 1, 24, @"http://www.thinkgeek.com/thinkgeek.rss"),
                    new NewsProvider(@"TED Talks", 1, 24, @"http://feeds.feedburner.com/tedtalks_video"),
                    new NewsProvider(@"YCombinator", 1, 24, @"https://news.ycombinator.com/rss"),
                    //new NewsProvider(@"", 1, 24, @""),
                };

            Console.WriteLine("Getting news...");

            var newsAggregator = new NewsAggregator();
            Dictionary<string, List<News>> news = newsAggregator.GetNews(providers);

            Console.WriteLine("Saving news...");

            using (var sw = new StreamWriter(@"C:\rss_notaccepted.txt"))
            {
                using (var swAccept = new StreamWriter(@"C:\rss_accepted.txt"))
                {
                    foreach (string newsprovider in news.Keys)
                    {
                        NewsProvider providerInfo = providers.FirstOrDefault(x => x.ProviderName == newsprovider);

                        foreach (var newsItem in news[newsprovider])
                        {
                            if (IsNewsAcceptable(newsItem, providerInfo))
                            {
                                swAccept.WriteLine(string.Format("{0} --- {1} --- {2}", providerInfo.ProviderName, newsItem.Header, DetectTheme(newsItem)));
                            }
                            else
                            {
                                // Debug.Assert(newsItem.Description.Length < 4000);
                                sw.WriteLine(string.Format("{0} --- {1}", providerInfo.ProviderName, newsItem.Header));
                            }
                        }
                    }
                }
            }
        }


        private static bool IsNewsAcceptable(News newsItem, NewsProvider providerInfo)
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

        private static string[] GetTokens(NewsProviderType type, int language)
        {
            if (type == NewsProviderType.Tech)
            {
                if (language == 1)
                {
                    return new[] {"clothe", "clothi", "wears", "weari", "smart-"};
                }

                return new[] {"одежд", "текстиль", "смарт-"};
            }
            else
            {
                if (language == 1)
                {
                    return new[] { "smart-", "3d-print", "3d" };
                }

                return new[] {"смарт", "умных", "умные", "умная", "смарт-", "3d-печат", "3d-принт"};
            }
        }

        private static string DetectTheme(News newsItem)
        {
            var possible = new Dictionary<NewsTags, string[]>()
            {
                {
                    NewsTags.Devices, new [] { "часы", "часов", "watch", "наушники", "phones", "audio", "звук", "аудио", "фитнес", "трекер", "fitnes", "track" }
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
    }
}
