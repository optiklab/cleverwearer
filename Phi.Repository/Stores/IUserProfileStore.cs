/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.Repository.Stores
{
    using Phi.Models.Models;
    using System;

    public interface IUserProfileStore
    {
        UserProfile GetUserProfileById(string userId);
        void Insert(UserProfile location);
        void Update(UserProfile location);
        void Delete(UserProfile location);
    }
}
