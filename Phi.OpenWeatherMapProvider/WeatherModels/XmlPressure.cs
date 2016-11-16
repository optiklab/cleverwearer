using System;
using System.Xml.Serialization;

namespace Phi.OpenWeatherMapProvider.WeatherModels
{
    public class XmlPressure
    {
        [XmlAttribute("value")]
        public string Value { get; set; }

        [XmlAttribute("unit")]
        public string Unit { get; set; }
    }
}
