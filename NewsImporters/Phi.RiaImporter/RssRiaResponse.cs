using System.Xml.Serialization;

namespace Phi.RiaImporter
{
    [XmlRoot(ElementName = "rss")]
    public class RssRiaResponse
    {
        [XmlElement("channel")]
        public XmlChannel Channel { get; set; }
    }
}
