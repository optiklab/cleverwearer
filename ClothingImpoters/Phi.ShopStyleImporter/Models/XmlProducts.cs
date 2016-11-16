using System.Collections.Generic;
using System.Xml.Serialization;

namespace Phi.ShopStyleImporter.Models
{
    public class XmlProducts
    {
        [XmlElement("product")]
        public List<XmlProduct> Products;
    }
}