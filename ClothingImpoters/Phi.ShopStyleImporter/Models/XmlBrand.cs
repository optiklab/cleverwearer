using System.Collections.Generic;
using System.Xml.Serialization;

namespace Phi.ShopStyleImporter.Models
{
    public class XmlBrand
    {
        [XmlElement("name")]
        public string Name;
    }
}
