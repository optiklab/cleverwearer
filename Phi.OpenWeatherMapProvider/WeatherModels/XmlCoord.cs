using System.Xml.Serialization;

namespace Phi.OpenWeatherMapProvider.WeatherModels
{
    public class XmlCoord
    {
        [XmlAttribute("lon")]
        public string Longitude { get; set; }

        [XmlAttribute("lat")]
        public string Latitude { get; set; }
    }
}
