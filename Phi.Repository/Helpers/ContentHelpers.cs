/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.Repository.Infrastructure
{
    using Phi.Repository.Enums;
    using System;

    public static class ContentHelpers
    {
        public static string GetLocalCurrencyName(int? currency)
        {
            if (currency == null)
                return string.Empty;

            CurrencyType currencyType = (CurrencyType)currency;

            char c = '\u20BD';
            switch(currencyType)
            {
                case CurrencyType.Rubles:
                    return c.ToString();
                case CurrencyType.Dollars:
                    return "$";
                case CurrencyType.Euro:
                    return "€";
                case CurrencyType.Funt:
                    return "£";
                case CurrencyType.Yena:
                    return "¥";
                default:
                    break;
            }

            return string.Empty;
        }
    }
}