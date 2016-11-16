using System.Xml.Serialization;

namespace Phi.RssLoader
{
    public class XmlEnclosure
    {
        [XmlAttribute("url")]
        public string Url { get; set; }

        [XmlAttribute("type")]
        public double Type { get; set; }
    }
}
