using System.Collections.Generic;
using System.Xml.Serialization;

namespace Phi.ShopStyleImporter.Models
{
    public class XmlSizes
    {
        [XmlElement("IPhone")]
        public XmlIPhone IPhone;
    }
}
