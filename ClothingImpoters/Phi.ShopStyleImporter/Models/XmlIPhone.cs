using System.Collections.Generic;
using System.Xml.Serialization;

namespace Phi.ShopStyleImporter.Models
{
    public class XmlIPhone
    {
        [XmlElement("url")]
        public string Url;
    }
}
