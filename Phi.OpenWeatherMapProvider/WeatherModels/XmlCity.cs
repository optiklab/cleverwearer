using System;
using System.Xml.Serialization;

namespace Phi.OpenWeatherMapProvider.WeatherModels
{
    public class XmlCity
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("name")]
        public string CityName { get; set; }

        [XmlElement("coord")]
        public XmlCoord Coord { get; set; }

        [XmlElement("sun")]
        public XmlSun Sun { get; set; }

        [XmlElement("country")]
        public string Country { get; set; }
    }
}
