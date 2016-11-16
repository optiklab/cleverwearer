using System.Collections.Generic;
using System.Xml.Serialization;

namespace Phi.ShopStyleImporter.Models
{
    [XmlRoot("ProductListResponse")]
    public class XmlProductListResponse
    {
        [XmlElement("metadata")]
        public XmlMetadata Metadata;

        [XmlElement("products")]
        public XmlProducts Products;
    }
}
