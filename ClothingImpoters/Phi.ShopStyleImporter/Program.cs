using Phi.ShopStyleImporter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Phi.ShopStyleImporter
{
    public class Program
    {
        private const string ITEM_PROVIDER = "ShopStyle";

        private static Dictionary<string, List<string>> uris = new Dictionary<string, List<string>>
        {
            {
                "rain",
                new List<string>
                {
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=rain&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=rain+coat&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=rain+clothes&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=rain+shoes&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=rain+boots&offset=0&format=xml&limit=100"
                }
            },
            {
                "wind",
                new List<string>
                {
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=wind+clothes&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=wind+coat&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=wind&offset=0&format=xml&limit=100"
                }
            },
            {
                "autumn",
                new List<string>
                {
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=autumn+clothes&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=autumn&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=autumn+shoes&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=autumn+boots&offset=0&format=xml&limit=100"
                }
            },
            {
                "warm",
                new List<string>
                {
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=warm+clothes&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=warm&offset=0&format=xml&limit=100"
                }
            },
            {
                "winter",
                new List<string>
                {
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=winter+clothes&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=winter&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=winter+shoes&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=winter+boots&offset=0&format=xml&limit=100"
                }
            },
            {
                "sport",
                new List<string>
                {
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=sport+clothes&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=sport&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=t-shirt&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=tshirts&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=shorts&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=sneakers&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=sport+shoes&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=leggins&offset=0&format=xml&limit=100"
                }
            },
            {
                "office",
                new List<string>
                {
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=office+clothes&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=office+shoes&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=office&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=pant&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=blouse&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=suit&offset=0&format=xml&limit=100"
                }
            },
            {
                "walk",
                new List<string>
                {
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=walk+clothes&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=walk+shoes&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=walk&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=jeans&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=dress&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=skirt&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=jackets&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=cardigan&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=blouse&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=jumper&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=hoodie&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=sweatshirts&offset=0&format=xml&limit=100"
                }
            },
            {
                "kids",
                new List<string>
                {
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=kids+clothes&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=kids+shoes&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=kids&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=girl&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=boy&offset=0&format=xml&limit=100"
                }
            },
            {
                "travel",
                new List<string>
                {
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=travel+clothes&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=travel+shoes&offset=0&format=xml&limit=100",
                    "http://api.shopstyle.com/api/v2/products?pid=[TODO]&fts=travel&offset=0&format=xml&limit=100"
                }
            }
        };
        
        private static HashSet<string> categories = new HashSet<string>();
        private static Dictionary<string, string> items = new Dictionary<string, string>();

        public static void Main(string[] args)
        {
            StringBuilder sqlInsert = new StringBuilder();

            // First - add provider
            sqlInsert.AppendLine(string.Format(Constants.INSERT_PROVIDER, ITEM_PROVIDER));
            sqlInsert.AppendLine("GO");

            // Second - download items
            Task task = DownloadAndFillItems();

            task.Wait();
            
            // Third - add items and item types
            foreach (var category in categories)
            {
                sqlInsert.AppendLine(string.Format(Constants.INSERT_ITEM_TYPE, category, ITEM_PROVIDER));
            }
            sqlInsert.AppendLine("GO");

            foreach (var item in items)
            {
                sqlInsert.AppendLine(item.Value);
            }
            sqlInsert.AppendLine("GO");

            // DON'T FORGET 
            //INSERT [phi].[dbo].[ProvidersItems] ([ItemId],[ItemProvidersId]) SELECT {0},[PROVIDER_ID] FROM [phi].[dbo].[Item]"
            //GO

            using (FileStream fstr = File.Create(@"sqlShopStyleInsert.txt"))
            {
                using (StreamWriter sw = new StreamWriter(fstr))
                {
                    sw.Write(sqlInsert);
                }
            }

            Console.WriteLine("Finished");
        }

        private static async Task DownloadAndFillItems()
        {
            int total = 0;
            foreach (string uriGroup in uris.Keys)
            {
                total += uris[uriGroup].Count;
            }

            int current = 1;
            foreach (string uriGroup in uris.Keys)
            {
                foreach (string uri in uris[uriGroup])
                {
                    Console.WriteLine(string.Format("Downloading {0} from {1}.", current, total));

                    string body = string.Empty;
                    try
                    {
                        var httpRequest = (HttpWebRequest)WebRequest.Create(uri);

                        WebResponse webResponse = await httpRequest.GetResponseAsync();

                        var response = (HttpWebResponse)webResponse;

                        using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                        {
                            body = sr.ReadToEnd();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message + " " + ex.StackTrace);
                    }

                    XmlProductListResponse parsed = null;
                    try
                    {
                        parsed = FromXmlString(body);
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message + " " + ex.StackTrace);
                    }

                    switch (uriGroup)
                    {
                        case "rain":
                            {
                                Collect(parsed, "22", "1", false, "136", "75", "14");
                                Collect(parsed, "22", "1", false, "136", "75", "13");
                            }
                            break;
                        case "wind":
                            {
                                Collect(parsed, "28", "1", false, "56", "0", "14");
                                Collect(parsed, "28", "1", false, "56", "0", "13");
                            }
                            break;
                        case "autumn":
                            {
                                Collect(parsed, "20", "1", false, (2 + 4 + 8 + 128).ToString(), "50", "14");
                                Collect(parsed, "20", "1", false, (2 + 4 + 8 + 128).ToString(), "50", "13");
                            }
                            break;
                        case "warm":
                            {
                                Collect(parsed, "12", "1", false, (2 + 4 + 8).ToString(), "0", "14");
                                Collect(parsed, "12", "1", false, (2 + 4 + 8).ToString(), "0", "13");
                            }
                            break;
                        case "winter":
                            {
                                Collect(parsed, "8", "1", false, (2 + 4 + 8).ToString(), "50", "14");
                                Collect(parsed, "8", "1", false, (2 + 4 + 8).ToString(), "50", "13");
                            }
                            break;
                        case "sport":
                            {
                                Collect(parsed, "30", "1", false, (2 + 4 + 8 + 256 + 1024 + 2048).ToString(), "0", "3");
                                Collect(parsed, "30", "1", false, (2 + 4 + 8 + 256 + 1024 + 2048).ToString(), "0", "4");
                            }
                            break;
                        case "office":
                            {
                                Collect(parsed, "30", "1", false, (2 + 4 + 8 + 256 + 1024 + 2048).ToString(), "0", "9");
                                Collect(parsed, "30", "1", false, (2 + 4 + 8 + 256 + 1024 + 2048).ToString(), "0", "10");
                            }
                            break;
                        case "walk":
                            {
                                Collect(parsed, "28", "1", false, (2 + 4 + 8 + 256 + 1024 + 2048).ToString(), "25", "14");
                                Collect(parsed, "28", "1", false, (2 + 4 + 8 + 256 + 1024 + 2048).ToString(), "25", "13");
                            }
                            break;
                        case "kids":
                            {
                                Collect(parsed, "28", "1", true, (2 + 4 + 8).ToString(), "0", "11");
                                Collect(parsed, "28", "1", true, (2 + 4 + 8).ToString(), "0", "12");
                            }
                            break;
                        case "travel":
                            {
                                Collect(parsed, "28", "1", false, (2 + 4 + 8).ToString(), "0", "15");
                                Collect(parsed, "28", "0", false, (2 + 4 + 8).ToString(), "0", "16");
                            }
                            break;
                        default:
                            break;
                    }

                    ++current;
                }
            }
        }

        private static void Collect(XmlProductListResponse parsed, string season = "22", string gender = "1", bool isChild = false, string suggestionTerms = "12", string waterProtection = "0", string actionType = "14")
        {
            foreach (var item in parsed.Products.Products)
            {
                if (items.Keys.Any(x => x == item.Id))
                    continue;

                var category = item.Categories.List.FirstOrDefault();

                if (category == null)
                    continue;

                string categoryName = category.Name.Replace("'", "''");

                categories.Add(categoryName);

                string description = item.Description.Replace("'", "''");
                string name = item.Name.Replace("'", "''");
                string retailerName = item.Retailer.Name.Replace("'", "''");

                items.Add(item.Id,
                    string.Format(Constants.INSERT_ITEMS,
                        categoryName,
                        actionType,
                        description,
                        name,
                        1,
                        season,
                        gender,
                        "0", // Min Age
                        isChild ? "18" : "100", // Max Age
                        waterProtection,
                        "0",
                        "0",
                        "0",
                        retailerName,
                        ITEM_PROVIDER,
                        suggestionTerms,
                        "1", //IsPublic
                        item.Image.Sizes.IPhone.Url,
                        isChild ? "1" : "0",
                        "1", // IsAvailable
                        item.Price.ToString().Replace(",","."),
                        item.ClickUrl,
                        "0", // Currency
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'",
                        "NULL", //AvailableTill
                        ITEM_PROVIDER));
            }
        }

        private static XmlProductListResponse FromXmlString(string xml)
        {
            using (var xmlRdr = new StringReader(xml))
            {
                var srlzr = new XmlSerializer(typeof(XmlProductListResponse));
                return (XmlProductListResponse)srlzr.Deserialize(xmlRdr);
            }
        }
    }
}
