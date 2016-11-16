using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Phi.OpenWeatherMapProvider.WeatherModels
{
    public class XmlForecast
    {
        [XmlElement("time")]
        public List<XmlTime> Times { get; set; }
    }
}

