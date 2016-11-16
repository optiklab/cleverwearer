using System.Collections.Generic;
using System.Xml.Serialization;

namespace Phi.ShopStyleImporter.Models
{
    public class XmlProduct
    {
        [XmlElement("id")]
        public string Id;

        [XmlElement("name")]
        public string Name;

        [XmlElement("priceLabel")]
        public string PriceLabel;

        [XmlElement("retailer")]
        public XmlRetailer Retailer;

        [XmlElement("brand")]
        public XmlBrand Brand;

        [XmlElement("description")]
        public string Description;

        [XmlElement("clickUrl")]
        public string ClickUrl;

        [XmlElement("pageUrl")]
        public string PageUrl;

        [XmlElement("image")]
        public XmlImage Image;

        [XmlElement("price")]
        public float Price;

        [XmlElement("categories")]
        public XmlCategories Categories;

        [XmlElement("promotionalDeal")]
        public XmlPromotionalDeal PromotionalDeal;
    }
}