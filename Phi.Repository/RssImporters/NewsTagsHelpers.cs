
namespace Phi.Repository.RssImporters
{
    public class NewsTagsHelpers
    {
        public static string GetTagDescription(NewsTags tag)
        {
            switch (tag)
            {
                case NewsTags.Clothes:
                    return "#smartclothes";
                case NewsTags.Startups:
                    return "#startups";
                case NewsTags.Devices:
                    return "#devices";
                case NewsTags.Robots:
                    return "#robots";
                case NewsTags.AI:
                    return "#ai";
                case NewsTags.Video:
                    return "#video";
                default:
                    return "#commons";
            }
        }
    }
}
