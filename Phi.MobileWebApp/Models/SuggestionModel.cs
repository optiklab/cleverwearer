/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.MobileWebApp.Models
{
    using System;

    public class SuggestionModel
    {
        #region Suggestions items

        //public string ShortDescription { get; set; }
        //public string FullDescription { get; set; }

        public string ImageUrl { get; set; }
        public string ReferrerUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MadeBy { get; set; }
        public string ProvideBy { get; set; }
        public string Language { get; set; }
        public Boolean Gender { get; set; }
        public string Season { get; set; }
        public int WaterProtectionPercent { get; set; }
        public Boolean IceProtection { get; set; }
        public int ArmoringPercent { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public int SunProtectionPercent { get; set; }
        public string ActionType { get; set; }
        public string Price { get; set; }
        public string ItemType { get; set; }

        public Boolean IsWardrobe { get; set; }

        #endregion
    }
}