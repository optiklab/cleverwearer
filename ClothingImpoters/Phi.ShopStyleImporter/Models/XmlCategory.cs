using System.Collections.Generic;
using System.Xml.Serialization;

namespace Phi.ShopStyleImporter.Models
{
    public class XmlCategory
    {
        [XmlElement("id")]
        public string Id;

        [XmlElement("name")]
        public string Name;

        [XmlElement("numId")]
        public int NumId;
    }
}
