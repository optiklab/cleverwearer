using System.Collections.Generic;
using System.Xml.Serialization;

namespace Phi.ShopStyleImporter.Models
{
    public class XmlDate
    {
        [XmlElement("date")]
        public string Date;

        [XmlElement("timestamp")]
        public string Timestamp;

        [XmlElement("friendly")]
        public string FriendlyDate;
    }
}

