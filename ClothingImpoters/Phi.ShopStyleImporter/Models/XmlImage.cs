using System.Collections.Generic;
using System.Xml.Serialization;

namespace Phi.ShopStyleImporter.Models
{
    public class XmlImage
    {
        [XmlElement("id")]
        public string Id;

        [XmlElement("sizes")]
        public XmlSizes Sizes;
    }
}
