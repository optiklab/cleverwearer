using System.Xml.Serialization;

namespace Phi.OpenWeatherMapProvider.WeatherModels
{
    public class XmlPrecipitation
    {
        [XmlAttribute("mode")]
        public string Mode { get; set; }
    }
}
