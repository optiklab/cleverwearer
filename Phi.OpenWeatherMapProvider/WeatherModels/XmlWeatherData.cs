using System.Xml.Serialization;

namespace Phi.OpenWeatherMapProvider.WeatherModels
{
    [XmlRoot(ElementName = "weatherdata")]
    public class XmlWeatherData
    {
        [XmlElement("location")]
        public XmlCity Location { get; set; }

        [XmlElement("sun")]
        public XmlSun Sun { get; set; }

        [XmlElement("forecast")]
        public XmlForecast Forecast { get; set; }
    }
}
