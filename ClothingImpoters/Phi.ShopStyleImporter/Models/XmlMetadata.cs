using System.Collections.Generic;
using System.Xml.Serialization;

namespace Phi.ShopStyleImporter.Models
{
    public class XmlMetadata
    {
        [XmlElement("total")]
        public int Total;
    }
}
