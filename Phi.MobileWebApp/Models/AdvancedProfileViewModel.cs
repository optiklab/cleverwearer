/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/
namespace Phi.MobileWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using Phi.MobileWebApp.Resources;
    using Phi.MobileWebApp.Validation;

    public class AdvancedProfileViewModel
    {
        public Boolean NotifyAboutWeatherEvents { get; set; }

        [ValidLength(Length = 255, ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "CompanyNameLenghtLimit")]
        public string CompanyName { get; set; }

        [ValidLength(Length = 255, ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "CompanyCEOLenghtLimit")]
        public string CompanyCEO { get; set; }

        [ValidLength(Length = 50, ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "CompanyPhoneLenghtLimit")]
        [Phone]
        public string CompanyPhone { get; set; }

        [ValidLength(Length = 255, ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "CompanyEmailLenghtLimit")]
        [EmailAddress]
        public string CompanyEmail { get; set; }

        [ValidLength(Length = 50, ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "CompanyFaxLenghtLimit")]
        [Phone]
        public string CompanyFax { get; set; }

        [ValidLength(Length = 255, ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "MainCompanyUrlLenghtLimit")]
        public string MainCompanyUrl { get; set; }

        [ValidLength(Length = 255, ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "SellCompanyUrlLenghtLimit")]
        public string SellCompanyUrl { get; set; }

        [ValidLength(Length = 2000, ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "AdditionalInfoLenghtLimit")]
        public string AdditionalInfo { get; set; }
    }
}