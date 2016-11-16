using System;
using System.Xml.Serialization;

namespace Phi.OpenWeatherMapProvider.WeatherModels
{
    public class XmlLastUpdate
    {
        [XmlAttribute("value")]
        public string Value { get; set; }
    }
}
