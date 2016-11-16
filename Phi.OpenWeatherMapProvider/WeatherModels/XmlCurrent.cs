using System.Xml.Serialization;

namespace Phi.OpenWeatherMapProvider.WeatherModels
{
    [XmlRoot(ElementName = "current")]
    public class XmlCurrent
    {        
        [XmlElement("city")]
        public XmlCity City { get; set; }

        [XmlElement("temperature")]
        public XmlTemperature Temperature { get; set; }

        [XmlElement("humidity")]
        public XmlHumidity Humidity { get; set; }

        [XmlElement("pressure")]
        public XmlPressure Pressure { get; set; }

        [XmlElement("wind")]
        public XmlWind Wind { get; set; }

        [XmlElement("clouds")]
        public XmlClouds Clouds { get; set; }

        [XmlElement("visibility")]
        public XmlVisibility Visibility { get; set; }

        [XmlElement("precipitation")]
        public XmlPrecipitation Precipitation { get; set; }

        [XmlElement("weather")]
        public XmlWeather Weather { get; set; }

        [XmlElement("lastupdate")]
        public XmlLastUpdate LastUpdate { get; set; }
    }
}
