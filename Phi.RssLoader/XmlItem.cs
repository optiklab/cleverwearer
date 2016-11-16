using System.Collections.Generic;
using System.Xml.Serialization;

namespace Phi.RssLoader
{
    public class XmlItem
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("link")]
        public string Link { get; set; }

        [XmlElement("pubDate")]
        public string PubDate { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("category")]
        public List<string> Category { get; set; }

        [XmlElement("guid")]
        public string Guid { get; set; }

        [XmlElement("enclosure")]
        public List<XmlEnclosure> Enclosure { get; set; }
    }
}
