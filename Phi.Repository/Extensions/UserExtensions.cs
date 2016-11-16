/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/
using System;
using System.Linq;
using Phi.Models.Models;
using Phi.Repository.Constants;

namespace Phi.Repository.Extensions
{
    public static class UserExtensions
    {
        /// <summary>
        /// Gets a value indicating whether user is in a certain user role
        /// </summary>
        /// <param name="user">PhiUser</param>
        /// <param name="userRoleSystemName">PhiUser role system name</param>
        /// <returns>Result</returns>
        public static bool IsInUserRole(this PhiUser user, string userRoleSystemName)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (String.IsNullOrEmpty(userRoleSystemName))
                throw new ArgumentNullException("userRoleSystemName");

            var result = user.UserRoles
                .Where(cr => cr.Role.Active)
                .Where(cr => cr.Role.Name == userRoleSystemName)
                .FirstOrDefault() != null;

            return result;
        }

        /// <summary>
        /// Gets a value indicating whether user is registered
        /// </summary>
        /// <param name="user">PhiUser</param>
        /// <returns>Result</returns>
        public static bool IsRegistered(this PhiUser user)
        {
            return IsInUserRole(user, SystemUserRoleNames.Registered);
        }

        /// <summary>
        /// Gets attribute of a user by its key.
        /// </summary>
        public static string GetAttribute(this PhiUser user, string key)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var userAttribute = user.UserAttributes
                .FirstOrDefault(ca => ca.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase));

            if (userAttribute == null)
                return null;

            if (string.IsNullOrEmpty(userAttribute.Value))
                return null;

            return userAttribute.Value;
        }
    }
}
