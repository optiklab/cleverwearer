/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/
namespace Phi.Repository.Enums
{
    /// <summary>
    /// Represents an user type record
    /// </summary>
    public enum UserType
    {
        // Guest (not registered)
        Anonymous = 0,

        // Simple user
        Registered = 1,

        // Games developer
        Developer = 2,

        // Moderator
        Moderator = 3
    }
}
