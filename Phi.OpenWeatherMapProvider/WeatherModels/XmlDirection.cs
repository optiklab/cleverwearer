using System.Xml.Serialization;

namespace Phi.OpenWeatherMapProvider.WeatherModels
{
    public class XmlDirection
    {
        [XmlAttribute("value")]
        public string Value { get; set; }

        [XmlAttribute("code")]
        public string Code { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}
