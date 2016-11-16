using System.Xml.Serialization;

namespace Phi.RssLoader
{
    [XmlRoot(ElementName = "rss")]
    public class RssResponse
    {
        [XmlElement("channel")]
        public XmlChannel Channel { get; set; }
    }
}
