/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/
using System;
using System.ComponentModel.DataAnnotations;
using Phi.MobileWebApp.Validation;
using Phi.MobileWebApp.Resources;

namespace Phi.MobileWebApp.Models
{
    /// <summary>
    /// Model for Profile View.
    /// </summary>
    public class ProfileViewModel
    {
        [Required(ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "UserNameRequired")]
        [ValidLength(Length = 50, ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "UserNameLenghtLimit")]
        public string UserName { get; set; }

        [ValidLength(Length = 50, ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "FirstNameLenghtLimit")]
        public string FirstName { get; set; }

        [ValidLength(Length = 50, ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "LastNameLenghtLimit")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [ValidLength(Length = 100, ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "EmailLenghtLimit")]
        public string Email { get; set; }

        [ValidLength(Length = 255, ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "UrlLenghtLimit")]
        public string Url { get; set; }

        [ValidLength(Length = 255)]
        public string AvatarPictureUrl { get; set; }

        [ValidLength(Length = 255)]
        public string Location { get; set; }

        public Boolean IsCheckedLocation { get; set; }

        public Boolean? Gender { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateCreated { get; set; }
    }
}