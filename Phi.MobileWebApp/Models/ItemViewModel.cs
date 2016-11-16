/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/
using System;
using System.ComponentModel.DataAnnotations;
/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
 */

namespace Phi.MobileWebApp.Models
{
    using System.Collections.Generic;
    using Phi.MobileWebApp.Validation;
    using Phi.MobileWebApp.Resources;
    using Phi.Models.Models;
    using System.Web.Mvc;

    /// <summary>
    /// Model for Profile View.
    /// </summary>
    public class ItemViewModel
    {
        public ItemViewModel()
        {
            Id = -1;
            ActionTypes = new List<ActionType>();

            var years = new List<int>();
            int currentYear = DateTime.Now.Year;
            int minYear = DateTime.MinValue.Year;
            for (int i = currentYear; i >= minYear; i--)
            {
                years.Add(i);
            }
            Years = new SelectList(years);
            Gender = false;
        }

        /// <summary>
        /// TODO AY. Probably, for security reasons we should not have IDs in the View Model and replace it by some other
        /// ID-like string which can be then calculated by both client and server.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Set true if image was loaded first time or reloaded. Set false or null in other case.
        /// We use this flag during editing of item to make sure if we need to make changes in Images table.
        /// </summary>
        public bool? ImageReloaded { get; set; }

        [Required]
        [ValidLength(Length = 255, ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "NameLenghtLimit")]
        public string Name { get; set; }
        [Required]
        [ValidLength(Length = 2000, ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "DescriptionLenghtLimit")]
        public string Description { get; set; }
        [Required]
        public bool? Gender { get; set; }
        [Required]
        public int? SuggestionTermsTemperature { get; set; }
        [Required]
        public int? Season { get; set; }
        [Required]
        public int? ActionTypeId { get; set; }
        public int SuggestionTermsAdditional { get; set; }
        public int SuggestionTermsExtra { get; set; }
        public int SuggestionTerms { get; set; }

        public string MadeBy { get; set; }

        public string ProvideBy { get; set; }

        public int WaterProtectionPercent { get; set; }

        public bool IceProtection { get; set; }

        public int ArmoringPercent { get; set; }

        public int SunProtectionPercent { get; set; }

        public int MinAge { get; set; }

        public int MaxAge { get; set; }

        public SelectList Years { get; set; }
        public int Year { get; set; }

        public bool IsPublic { get; set; }

        public List<ActionType> ActionTypes { get; set; }

        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>
        /// The image URL.
        /// </value>
        [ValidLength(Length = 255)]
        public string ImageUrl { get; set; }
    }
}