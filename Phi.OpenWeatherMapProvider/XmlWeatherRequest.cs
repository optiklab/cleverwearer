using System.Diagnostics;

namespace Phi.OpenWeatherMapProvider
{
    public class XmlWeatherRequest
    {
        private const string REQUEST = "http://api.openweathermap.org/data/2.5/weather?id={0}&units={1}&mode=xml&appid=c47e6a2a4ef2cd520d735ca4cdda9d93";

        public XmlWeatherRequest(string woeId, string system)
        {
            Debug.Assert(system == "metric" || system == "imperial");

            _parametersUrl = string.Format(REQUEST, woeId, system);
        }

        public string GetUrl()
        {
            return _parametersUrl;
        }

        private string _parametersUrl;
    }
}
