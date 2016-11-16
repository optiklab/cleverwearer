using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace Phi.Models.Models
{
    public partial class PhiUser : IUser
    {
        public PhiUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.BlogComments = new List<BlogComment>();
            this.BlogStars = new List<BlogStar>();
            this.UserAttributes = new List<UserAttribute>();
            this.UserLogins = new List<UserLogin>();
            this.UserClaims = new List<UserClaim>();
            this.UserRoles = new List<UserRole>();
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ReminderQuestion { get; set; }
        public string ReminderAnswer { get; set; }
        public string PasswordSalt { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> LastLoggedDate { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public bool Active { get; set; }
        public Nullable<int> UserType { get; set; }
        public Nullable<int> UserNameFormat { get; set; }
        public Nullable<int> PasswordFormat { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public virtual ICollection<BlogComment> BlogComments { get; set; }
        public virtual ICollection<BlogStar> BlogStars { get; set; }
        public virtual ICollection<UserAttribute> UserAttributes { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }
        public virtual ICollection<UserClaim> UserClaims { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
