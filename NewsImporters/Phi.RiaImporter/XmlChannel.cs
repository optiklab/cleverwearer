using System.Collections.Generic;
using System.Xml.Serialization;

namespace Phi.RiaImporter
{
    public class XmlChannel
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("link")]
        public string Link { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("language")]
        public string Language { get; set; }

        [XmlElement("LastBuildDate")]
        public string lastBuildDate { get; set; }

        [XmlElement("pubDate")]
        public string Ttl { get; set; }

        [XmlElement("image")]
        public XmlImage Image { get; set; }

        [XmlElement("item")]
        public List<XmlItem> Item { get; set; }
    }
}
