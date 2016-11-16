/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.Repository.Stores
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Phi.Models.Models;
    using Microsoft.AspNet.Identity;

    public class UserStore : IUserStore<PhiUser>,
                             IUserClaimStore<PhiUser>,
                             IUserLoginStore<PhiUser>,
                             IUserRoleStore<PhiUser>,
                             IUserPasswordStore<PhiUser>,
                             IUserEmailStore<PhiUser>,
                             IUserProfileStore
    {
        #region Private fields

        /// <summary>
        /// The user repository.
        /// </summary>
        private readonly IRepository<PhiUser> _usersRepository;

        /// <summary>
        /// The role repository.
        /// </summary>
        private readonly IRepository<Role> _rolesRepository;

        /// <summary>
        /// The user roles repository.
        /// </summary>
        private readonly IRepository<UserRole> _userRolesRepository;

        /// <summary>
        /// The user claim repository.
        /// </summary>
        private readonly IRepository<UserClaim> _userClaimRepository;

        /// <summary>
        /// The user login repository.
        /// </summary>
        private readonly IRepository<UserLogin> _userLoginRepository;

        /// <summary>
        /// The user profile repository.
        /// </summary>
        private readonly IRepository<UserProfile> _userProfileRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UserStore"/> class.
        /// </summary>
        /// <param name="usersRepository">The users repository.</param>
        /// <param name="rolesRepository">The roles repository.</param>
        /// <param name="userRolesRepository">The user roles repository.</param>
        /// <param name="claimsRepository">The claims repository.</param>
        /// <param name="loginsRepository">The logins repository.</param>
        /// <param name="userProfileRepository">The user profile repository.</param>
        public UserStore(
            IRepository<PhiUser> usersRepository,
            IRepository<Role> rolesRepository,
            IRepository<UserRole> userRolesRepository,
            IRepository<UserClaim> claimsRepository,
            IRepository<UserLogin> loginsRepository,
            IRepository<UserProfile> userProfileRepository)
        {
            this._usersRepository = usersRepository;
            this._rolesRepository = rolesRepository;
            this._userRolesRepository = userRolesRepository;
            this._userClaimRepository = claimsRepository;
            this._userLoginRepository = loginsRepository;
            this._userProfileRepository = userProfileRepository;
        }

        #endregion

        #region IUserStore
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Insert a new PhiUser in the UserTable.
        /// </summary>
        /// <param name="user">Current user.</param>
        /// <returns>Null task.</returns>
        /// <exception cref="ArgumentNullException">User is null.</exception>
        public Task CreateAsync(PhiUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            this._usersRepository.Insert(user);

            return Task.FromResult<Object>(null);
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="user">Current user.</param>
        /// <returns>Task with result.</returns>
        public Task DeleteAsync(PhiUser user)
        {
            if (user != null)
            {
                this._usersRepository.Delete(user);
            }

            return Task.FromResult<Object>(null);
        }

        /// <summary>
        /// Returns an PhiUser instance based on a userId query.
        /// </summary>
        /// <param name="userId">The user's Id.</param>
        /// <returns>Task with result.</returns>
        /// <exception cref="ArgumentNullException">User id is null.</exception>
        public Task<PhiUser> FindByIdAsync(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("Null or empty argument: userId");
            }

            PhiUser result = this._usersRepository.GetById(userId);

            return Task.FromResult<PhiUser>(result);
        }

        /// <summary>
        /// Returns an PhiUser instance based on a userName query.
        /// </summary>
        /// <param name="userName">The user's nam.</param>
        /// <returns>Task with result.</returns>
        /// <exception cref="ArgumentNullException">Username is null.</exception>
        public Task<PhiUser> FindByNameAsync(string userName)
        {
            if (String.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Null or empty argument: userName");
            }

            var list = this._usersRepository.Table.ToList();
            List<PhiUser> result = list.Where(c => c.UserName == userName).ToList(); // TODO ToListAsync?

            // Should I throw if > 1 user?
            if (result.Count == 1)
            {
                return Task.FromResult<PhiUser>(result[0]);
            }

            return Task.FromResult<PhiUser>(null);
        }

        /// <summary>
        /// Updates the UsersTable with the PhiUser instance values.
        /// </summary>
        /// <param name="user">PhiUser to be updated.</param>
        /// <returns>Task with result.</returns>
        /// <exception cref="ArgumentNullException">User is null.</exception>
        public Task UpdateAsync(PhiUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            this._usersRepository.Update(user);

            return Task.FromResult<Object>(null);
        }

        #endregion

        #region IUserClaimStore

        /// <summary>
        /// Inserts a claim to the UserClaimsTable for the given user.
        /// </summary>
        /// <param name="user">User to have claim adde.</param>
        /// <param name="claim">Claim to be adde.</param>
        /// <returns>Task with result.</returns>
        /// <exception cref="ArgumentNullException">User or claim is null.</exception>
        public Task AddClaimAsync(PhiUser user, Claim claim)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (claim == null)
            {
                throw new ArgumentNullException("user");
            }

            var userClaim = new UserClaim
                                {
                                    ClaimValue = claim.Value,
                                    ClaimType = claim.Type,
                                    PhiUserId = user.Id
                                };

            this._userClaimRepository.Insert(userClaim);

            return Task.FromResult<Object>(null);
        }

        /// <summary>
        /// Returns all claims for a given user.
        /// </summary>
        /// <param name="user">Current user.</param>
        /// <returns>Task with result.</returns>
        /// <exception cref="ArgumentNullException">User is null.</exception>
        public Task<IList<Claim>> GetClaimsAsync(PhiUser user)
        {
            return Task.FromResult<IList<Claim>>(this._userClaimRepository.Table.ToList()
                .Where(c => c.PhiUserId == user.Id)
                .Select(x => new Claim(x.ClaimType, x.ClaimValue))
                .ToList()); // TODO ToListAsync?
        }

        /// <summary>
        /// Removes a claim froma user.
        /// </summary>
        /// <param name="user">User to have claim removed.</param>
        /// <param name="claim">Claim to be removed.</param>
        /// <returns>Task with result.</returns>
        /// <exception cref="ArgumentNullException">User or claim is null.</exception>
        public Task RemoveClaimAsync(PhiUser user, Claim claim)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }

            var userClaim = this._userClaimRepository.Table.ToList()
                .FirstOrDefault(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);

            this._userClaimRepository.Delete(userClaim);

            return Task.FromResult<Object>(null);
        }

        #endregion

        #region IUserLoginStore

        /// <summary>
        /// Inserts a Login in the UserLoginsTable for a given User.
        /// </summary>
        /// <param name="user">User to have login added.</param>
        /// <param name="login">Login to be added.</param>
        /// <returns>Task with result.</returns>
        /// <exception cref="ArgumentNullException">User or login is null.</exception>
        public Task AddLoginAsync(PhiUser user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            var userLogin = new UserLogin
            {
                PhiUserId = user.Id,
                ProviderKey = login.ProviderKey,
                LoginProvider = login.LoginProvider
            };

            this._userLoginRepository.Insert(userLogin);

            return Task.FromResult<Object>(null);
        }

        /// <summary>
        /// Returns an PhiUser based on the Login info.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <returns>Task with result.</returns>
        /// <exception cref="ArgumentNullException">Login is null.</exception>
        public Task<PhiUser> FindAsync(UserLoginInfo login)
        {
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            var userLogin = this._userLoginRepository.Table.ToList()
                .FirstOrDefault(c => c.LoginProvider == login.LoginProvider && c.ProviderKey == login.ProviderKey); // TODO ToListAsync?

            if (userLogin != null)
            {
                PhiUser user = this._usersRepository.GetById(userLogin.PhiUserId);
                if (user != null)
                {
                    return Task.FromResult<PhiUser>(user);
                }
            }

            return Task.FromResult<PhiUser>(null);
        }

        /// <summary>
        /// Returns list of UserLoginInfo for a given PhiUser.
        /// </summary>
        /// <param name="user">Current user.</param>
        /// <returns>Task with result.</returns>
        /// <exception cref="ArgumentNullException">User is null.</exception>
        public Task<IList<UserLoginInfo>> GetLoginsAsync(PhiUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var userLogins =
                this._userLoginRepository.Table.ToList()
                    .Where(c => c.PhiUserId == user.Id)
                    .Select(x => new UserLoginInfo(x.LoginProvider, x.ProviderKey))
                    .ToList();
            if (userLogins.Any())
            {
                return Task.FromResult<IList<UserLoginInfo>>(userLogins);
            }

            return Task.FromResult<IList<UserLoginInfo>>(null);
        }

        /// <summary>
        /// Deletes a login from UserLoginsTable for a given PhiUser.
        /// </summary>
        /// <param name="user">User to have login remove.</param>
        /// <param name="login">Login to be remove.</param>
        /// <returns>Task with result.</returns>
        /// <exception cref="ArgumentNullException">User or login is null.</exception>
        public Task RemoveLoginAsync(PhiUser user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            var userLogin = this._userLoginRepository.Table.ToList()
                .FirstOrDefault(c => c.LoginProvider == login.LoginProvider && c.ProviderKey == login.ProviderKey); // TODO ToListAsync?

            if (userLogin != null)
            {
                this._userLoginRepository.Delete(userLogin);
            }

            return Task.FromResult<Object>(null);
        }

        #endregion

        #region IUserRoleStore

        /// <summary>
        /// Inserts a entry in the UserRoles table.
        /// </summary>
        /// <param name="user">User to have role adde.</param>
        /// <param name="roleName">Name of the role to be added to use.</param>
        /// <returns>Task with result.</returns>
        /// <exception cref="ArgumentNullException">User or rolename is null.</exception>
        public Task AddToRoleAsync(PhiUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (String.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Argument cannot be null or empty: roleName.");
            }

            var role = this._rolesRepository.Table.ToList().FirstOrDefault(x => x.Name == roleName);

            if (role != null && !String.IsNullOrEmpty(role.Id))
            {
                this._userRolesRepository.Insert(new UserRole
                    {
                        PhiUserId = user.Id,
                        RoleId = role.Id,
                        AddRoleDate = DateTime.UtcNow
                    });
            }

            return Task.FromResult<Object>(null);
        }

        /// <summary>
        /// Returns the roles for a given PhiUser.
        /// </summary>
        /// <param name="user">Current user.</param>
        /// <returns>Task with result.</returns>
        /// <exception cref="ArgumentNullException">User is null.</exception>
        public Task<IList<String>> GetRolesAsync(PhiUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            List<String> roles = this._userRolesRepository.Table.ToList().Where(x => x.PhiUserId == user.Id).Select(y => y.Role.Name).ToList();

            return Task.FromResult<IList<String>>(roles);
        }

        /// <summary>
        /// Verifies if a user is in a role.
        /// </summary>
        /// <param name="user">Current user.</param>
        /// <param name="role">Current role.</param>
        /// <returns>Task with result.</returns>
        /// <exception cref="ArgumentNullException">User or role is null.</exception>
        public Task<Boolean> IsInRoleAsync(PhiUser user, string role)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (String.IsNullOrEmpty(role))
            {
                throw new ArgumentNullException("role");
            }

            List<String> roles = this._userRolesRepository.Table.ToList()
                .Where(x => x.PhiUserId == user.Id)
                .Select(y => y.Role.Name)
                .ToList();

            return Task.FromResult<Boolean>(roles.Contains(role));
        }

        /// <summary>
        /// Removes a user from a role.
        /// </summary>
        /// <param name="user">Current user.</param>
        /// <param name="role">Current role.</param>
        /// <returns>Throw exception.</returns>
        /// <exception cref="NotImplementedException">Always thrown.</exception>
        public Task RemoveFromRoleAsync(PhiUser user, string role)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IUserPasswordStore

        /// <summary>
        /// Returns the PasswordHash for a given PhiUser.
        /// </summary>
        /// <param name="user">Current user.</param>
        /// <returns>Task with result.</returns>
        public Task<String> GetPasswordHashAsync(PhiUser user)
        {
            string passwordHash = String.Empty;
            if (user != null)
            {
                passwordHash = user.PasswordHash;
            }

            return Task.FromResult<String>(passwordHash);
        }

        /// <summary>
        /// Verifies if user has password.
        /// </summary>
        /// <param name="user">Current user.</param>
        /// <returns>Task with result.</returns>
        public Task<Boolean> HasPasswordAsync(PhiUser user)
        {
            var hasPassword = !String.IsNullOrEmpty(user.PasswordHash);

            return Task.FromResult<Boolean>(hasPassword);
        }

        /// <summary>
        /// Sets the password hash for a given PhiUser.
        /// </summary>
        /// <param name="user">Current user.</param>
        /// <param name="passwordHash">Password hash.</param>
        /// <returns>Task with result.</returns>
        public Task SetPasswordHashAsync(PhiUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;

            return Task.FromResult<Object>(null);
        }

        #endregion

        #region IUserProfileStore

        /// <summary>
        /// Gets the user profile by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public UserProfile GetUserProfileById(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                return null;
            }

            var userProfiles = this._userProfileRepository.Table.ToList();

            return userProfiles.FirstOrDefault(x => x.PhiUserId == userId);
        }

        /// <summary>
        /// Inserts the specified location.
        /// </summary>
        /// <param name="userProfile">The user profile.</param>
        /// <exception cref="System.ArgumentNullException">userProfile</exception>
        public void Insert(UserProfile userProfile)
        {
            if (userProfile == null)
            {
                throw new ArgumentNullException("userProfile");
            }

            this._userProfileRepository.Insert(userProfile);
        }

        /// <summary>
        /// Updates the specified location.
        /// </summary>
        /// <param name="userProfile">The user profile.</param>
        /// <exception cref="System.ArgumentNullException">userProfile</exception>
        public void Update(UserProfile userProfile)
        {
            if (userProfile == null)
            {
                throw new ArgumentNullException("userProfile");
            }

            this._userProfileRepository.Update(userProfile);
        }

        /// <summary>
        /// Deletes the specified location.
        /// </summary>
        /// <param name="userProfile">The user profile.</param>
        /// <exception cref="System.ArgumentNullException">userProfile</exception>
        public void Delete(UserProfile userProfile)
        {
            if (userProfile == null)
            {
                throw new ArgumentNullException("userProfile");
            }

            this._userProfileRepository.Delete(userProfile);
        }

        #endregion

        #region IUserEmailStore

        public Task<PhiUser> FindByEmailAsync(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Null or empty argument: email");
            }

            PhiUser result = this._usersRepository.Table.FirstOrDefault(x => x.Email == email);

            return Task.FromResult<PhiUser>(result);
        }

        public Task<String> GetEmailAsync(PhiUser user)
        {
            return Task.FromResult<String>(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(PhiUser user)
        {
            return Task.FromResult<bool>(user.EmailConfirmed);
        }

        public Task SetEmailAsync(PhiUser user, string email)
        {
            user.Email = email;

            this._usersRepository.Update(user);

            return Task.FromResult<Object>(null);
        }

        public Task SetEmailConfirmedAsync(PhiUser user, bool confirmed)
        {
            user.EmailConfirmed = confirmed;
            user.Active = confirmed;

            this._usersRepository.Update(user);

            return Task.FromResult<Object>(null);
        }

        #endregion
    }
}