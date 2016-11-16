using System;
using System.Xml.Serialization;

namespace Phi.OpenWeatherMapProvider.WeatherModels
{
    public class XmlWind
    {
        [XmlElement("speed")]
        public XmlSpeed Speed { get; set; }

        [XmlElement("gusts")]
        public XmlGusts Gusts { get; set; }

        [XmlElement("direction")]
        public XmlDirection Direction { get; set; }
    }
}
