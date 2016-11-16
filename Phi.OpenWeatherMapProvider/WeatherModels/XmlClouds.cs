﻿using System.Xml.Serialization;

namespace Phi.OpenWeatherMapProvider.WeatherModels
{
    public class XmlClouds
    {
        [XmlAttribute("value")]
        public string Value { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}
