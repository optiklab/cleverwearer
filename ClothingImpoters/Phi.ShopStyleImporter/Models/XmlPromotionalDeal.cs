using System.Collections.Generic;
using System.Xml.Serialization;

namespace Phi.ShopStyleImporter.Models
{
    public class XmlPromotionalDeal
    {
        [XmlElement("title")]
        public string Title;

        [XmlElement("startDate")]
        public XmlDate StartDate;

        [XmlElement("endDate")]
        public XmlDate EndDate;
    }
}
