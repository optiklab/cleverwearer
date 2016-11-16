using System.Collections.Generic;
using System.Xml.Serialization;

namespace Phi.ShopStyleImporter.Models
{
    public class XmlCategories
    {
        [XmlElement("category")]
        public List<XmlCategory> List;
    }
}
