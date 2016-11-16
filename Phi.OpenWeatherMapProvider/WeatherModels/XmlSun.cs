using System;
using System.Xml.Serialization;

namespace Phi.OpenWeatherMapProvider.WeatherModels
{
    public class XmlSun
    {
        [XmlAttribute("rise")]
        public string Rise { get; set; }

        [XmlAttribute("set")]
        public string Set { get; set; }
    }
}
