using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Phi.Repository.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveSpecialSymbols(this string source)
        {
            string finalValue = String.Empty;
            foreach (Char el in source)
            {
                if (Char.IsLetterOrDigit(el))
                {
                    finalValue += el;
                }
            }

            return finalValue;
        }
        public static HtmlString ToHtmlString(this string str)
        {
            return new HtmlString(str);
        }
    }
}
