using System;
using System.Xml.Serialization;

namespace Phi.OpenWeatherMapProvider.WeatherModels
{
    public class XmlHumidity
    {
        [XmlAttribute("value")]
        public string Value { get; set; }

        [XmlAttribute("unit")]
        public string Unit { get; set; }
    }
}
