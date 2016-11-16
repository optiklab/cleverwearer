using System.Xml.Serialization;

namespace Phi.OpenWeatherMapProvider.WeatherModels
{
    public class XmlTime
    {
        [XmlAttribute("from")]
        public string From { get; set; }

        [XmlAttribute("to")]
        public string To { get; set; }

        [XmlElement("symbol")]
        public XmlSymbol Symbol { get; set; }

        [XmlElement("precipitation")]
        public XmlPrecipitation Precipitation { get; set; }

        [XmlElement("direction")]
        public XmlDirection Direction { get; set; }

        [XmlElement("speed")]
        public XmlSpeed Speed { get; set; }

        [XmlElement("temperature")]
        public XmlTemperature Temperature { get; set; }

        [XmlElement("pressure")]
        public XmlPressure Pressure { get; set; }

        [XmlElement("humidity")]
        public XmlHumidity Humidity { get; set; }

        [XmlElement("clouds")]
        public XmlClouds Clouds { get; set; }
    }
}
