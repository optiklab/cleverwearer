using System.Xml.Serialization;

namespace Phi.OpenWeatherMapProvider.WeatherModels
{
    public class XmlTemperature
    {
        [XmlAttribute("value")]
        public string Value { get; set; }

        [XmlAttribute("min")]
        public string Min { get; set; }

        [XmlAttribute("max")]
        public string Max { get; set; }

        [XmlAttribute("unit")]
        public string Unit { get; set; }
    }
}
