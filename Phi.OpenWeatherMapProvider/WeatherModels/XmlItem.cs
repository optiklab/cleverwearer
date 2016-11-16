using System.Xml.Serialization;

namespace Phi.OpenWeatherMapProvider.WeatherModels
{
    public class XmlItem
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("lat", Namespace = "http://www.w3.org/2003/01/geo/wgs84_pos#")]
        public double Latitude { get; set; }

        [XmlElement("long", Namespace = "http://www.w3.org/2003/01/geo/wgs84_pos#")]
        public double Longitude { get; set; }

        [XmlElement("link")]
        public string Link { get; set; }

        [XmlElement("pubDate")]
        public string PubDate { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("condition", Namespace = "http://xml.weather.yahoo.com/ns/rss/1.0")]
        public XmlCoord Condition { get; set; }

        [XmlElement("forecast", Namespace = "http://xml.weather.yahoo.com/ns/rss/1.0")]
        public XmlTemperature[] Forecast { get; set; }

        [XmlElement("guid")]
        public XmlClouds Guid { get; set; }
    }
}
