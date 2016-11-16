/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.MobileWebApp.Models
{
    using Microsoft.AspNet.Identity;
    using Phi.Models.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web;

    public class PhiUserHelpers
    {
        public static async Task<ClaimsIdentity> GenerateUserIdentityAsync(PhiUser user, UserManager<PhiUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}