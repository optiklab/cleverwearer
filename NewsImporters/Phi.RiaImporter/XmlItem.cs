using System.Xml.Serialization;

namespace Phi.RiaImporter
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

        [XmlElement("guid")]
        public string Guid { get; set; }
    }
}
