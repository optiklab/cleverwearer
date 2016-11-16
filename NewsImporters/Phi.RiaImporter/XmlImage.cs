using System.Xml.Serialization;

namespace Phi.RiaImporter
{
    public class XmlImage
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("width")]
        public double Width { get; set; }

        [XmlElement("height")]
        public double Height { get; set; }

        [XmlElement("link")]
        public string Link { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }
    }
}
