/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.Repository.RssImporters
{
    public class NewsProvider
    {
        public NewsProvider(string providerName, int languageId, int updatePeriodHours, string url, NewsProviderType type = NewsProviderType.Tech)
        {
            ProviderName = providerName;
            LanguageId = languageId;
            UpdatePeriodHours = updatePeriodHours;
            Url = url;
            Type = type;
        }

        public NewsProviderType Type { get; private set; }
        public string ProviderName { get; private set; }
        public int LanguageId { get; private set; }
        public int UpdatePeriodHours { get; private set; }
        public string Url { get; private set; }
    }
}
