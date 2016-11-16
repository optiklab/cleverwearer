/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.MobileWebApp.Models
{
    using System;
    using Phi.MobileWebApp.Resources;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
using Phi.MobileWebApp.Validation;

    // Models returned by AccountController actions.
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(GlobalResources))]
        public string Email { get; set; }

        //[Display(Name = "Hometown")]
        //public string Hometown { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string Action { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "CurrentPassword", ResourceType = typeof(GlobalResources))]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "PasswordLenghtLimit", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType = typeof(GlobalResources))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmNewPassword", ResourceType = typeof(GlobalResources))]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "PasswordCompare")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(GlobalResources))]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(GlobalResources))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(GlobalResources))]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(GlobalResources))]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "PasswordLenghtLimit", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(GlobalResources))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(GlobalResources))]
        [Compare("Password", ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "PasswordCompare")]
        public string ConfirmPassword { get; set; }

        //[Display(Name = "Hometown")]
        //public string Hometown { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(GlobalResources))]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "PasswordLenghtLimit", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(GlobalResources))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(GlobalResources))]
        [Compare("Password", ErrorMessageResourceType = typeof(GlobalResources), ErrorMessageResourceName = "PasswordCompare")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(GlobalResources))]
        public string Email { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code", ResourceType = typeof(GlobalResources))]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "RememberBrowser", ResourceType = typeof(GlobalResources))]
        public bool RememberBrowser { get; set; }
    }
}