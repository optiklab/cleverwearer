using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Phi.OpenWeatherMapProvider.WeatherModels
{
    public class XmlSymbol
    {
        [XmlAttribute("number")]
        public string Number { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("var")]
        public string Var { get; set; }
    }
}

