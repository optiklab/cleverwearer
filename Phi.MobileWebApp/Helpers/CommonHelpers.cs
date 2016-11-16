/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.MobileWebApp.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;
    using Phi.Repository.Enums;
    using Phi.MobileWebApp.Resources;
    using System.Diagnostics;

    public class CommonHelpers
    {
        public static Boolean IsEmail(string identifier)
        {
            return Regex.IsMatch(identifier, ".+\\@.+\\..+");
        }

        public static string GetWindDirectionByAngle(Double angle)
        {
            if (angle > 348.75 || angle <= 11.25)
            {
                return GlobalResources.WindN;
            }
            else if (angle > 11.25 && angle <= 33.75)
            {
                return GlobalResources.WindNNE;
            }
            else if (angle > 33.75 && angle <= 56.25)
            {
                return GlobalResources.WindNE;
            }
            else if (angle > 56.25 && angle <= 78.75)
            {
                return GlobalResources.WindENE;
            }
            else if (angle > 78.75 && angle <= 101.25)
            {
                return GlobalResources.WindE;
            }
            else if (angle > 101.25 && angle <= 123.75)
            {
                return GlobalResources.WindESE;
            }
            else if (angle > 123.75 && angle <= 146.25)
            {
                return GlobalResources.WindSE;
            }
            else if (angle > 146.25 && angle <= 168.75)
            {
                return GlobalResources.WindSSE;
            }
            else if (angle > 168.75 && angle <= 191.25)
            {
                return GlobalResources.WindS;
            }
            else if (angle > 191.25 && angle <= 213.75)
            {
                return GlobalResources.WindSSW;
            }
            else if (angle > 213.75 && angle <= 236.25)
            {
                return GlobalResources.WindSW;
            }
            else if (angle > 236.25 && angle <= 258.75)
            {
                return GlobalResources.WindWSW;
            }
            else if (angle > 258.75 && angle <= 281.25)
            {
                return GlobalResources.WindW;
            }
            else if (angle > 281.25 && angle <= 303.75)
            {
                return GlobalResources.WindWNW;
            }
            else if (angle > 303.75 && angle <= 326.25)
            {
                return GlobalResources.WindNW;
            }
            else if (angle > 326.25 && angle <= 348.75)
            {
                return GlobalResources.WindNNW;
            }

            return String.Empty;
        }

        public static IDictionary<int, string> GetItemTypesForFilter(int languageId)
        {
            var items = new Dictionary<int, string>();
            items.Add(9, (languageId == 1) ? "All" : "Все");

            foreach (ItemTypeEnum itemType in Enum.GetValues(typeof(ItemTypeEnum)))
            {
                switch(itemType)
                {
                    case ItemTypeEnum.Tops:
                        items.Add(0, (languageId == 1) ? ItemTypeEnum.Tops.ToString() : "Верх");
                        break;
                    case ItemTypeEnum.Bottoms:
                        items.Add(1, (languageId == 1) ? ItemTypeEnum.Bottoms.ToString() : "Низ");
                        break;
                    case ItemTypeEnum.Shoes:
                        items.Add(2, (languageId == 1) ? ItemTypeEnum.Shoes.ToString() : "Обувь");
                        break;
                    case ItemTypeEnum.Accessories:
                        items.Add(3, (languageId == 1) ? ItemTypeEnum.Accessories.ToString() : "Аксессуары");
                        break;
                    default:
                        Debug.Assert(false);
                        break;
                }
            }

            return items;
        }
    }
}