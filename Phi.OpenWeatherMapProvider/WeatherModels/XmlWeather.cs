using System;
using System.Xml.Serialization;

namespace Phi.OpenWeatherMapProvider.WeatherModels
{
    public class XmlWeather
    {
        [XmlAttribute("number")]
        public string Number { get; set; }

        [XmlAttribute("value")]
        public string Value { get; set; }

        [XmlAttribute("icon")]
        public string Icon { get; set; }
    }
}
