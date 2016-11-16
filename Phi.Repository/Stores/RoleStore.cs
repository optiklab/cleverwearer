/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.Repository.Stores
{
    using Microsoft.AspNet.Identity;
    using Phi.Models.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RoleStore : IRoleStore<Role>
    {
        #region Private fields

        /// <summary>
        /// The _users repository.
        /// </summary>
        private readonly IRepository<PhiUser> _usersRepository;

        /// <summary>
        /// The _roles repository.
        /// </summary>
        private readonly IRepository<Role> _rolesRepository;

        /// <summary>
        /// The _user roles repository.
        /// </summary>
        private readonly IRepository<UserRole> _userRolesRepository;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleStore"/> class.
        /// </summary>
        /// <param name="usersRepository">The users repository.</param>
        /// <param name="rolesRepository">The roles repository.</param>
        /// <param name="userRolesRepository">The user roles repository.</param>
        public RoleStore(
            IRepository<PhiUser> usersRepository,
            IRepository<Role> rolesRepository,
            IRepository<UserRole> userRolesRepository)
        {
            this._usersRepository = usersRepository;
            this._rolesRepository = rolesRepository;
            this._userRolesRepository = userRolesRepository;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Inserts role in asynchronous mode.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>Task result.</returns>
        /// <exception cref="System.ArgumentNullException">Role is null.</exception>
        public Task CreateAsync(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            _rolesRepository.Insert(role);

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Deletes role in asynchronous mode.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>Task result.</returns>
        /// <exception cref="System.ArgumentNullException">Role is null.</exception>
        public Task DeleteAsync(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("user");
            }

            _rolesRepository.Delete(role);

            return Task.FromResult<Object>(null);
        }

        /// <summary>
        /// Searches role by Id in asynchronous mode.
        /// </summary>
        /// <param name="role">The role Id.</param>
        /// <returns>Task result.</returns>
        /// <exception cref="System.ArgumentNullException">Role Id is null.</exception>
        public Task<Role> FindByIdAsync(string roleId)
        {
            if (String.IsNullOrEmpty(roleId))
            {
                throw new ArgumentNullException("roleId");
            }

            Role result = _rolesRepository.GetById(roleId);

            return Task.FromResult<Role>(result);
        }


        /// <summary>
        /// Searches role by name in asynchronous mode.
        /// </summary>
        /// <param name="role">The role name.</param>
        /// <returns>Task result.</returns>
        /// <exception cref="System.ArgumentNullException">Rolename is null.</exception>
        public Task<Role> FindByNameAsync(string roleName)
        {
            if (String.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException("roleName");
            }

            Role result = this._rolesRepository.Table.ToList().FirstOrDefault(x => x.Name == roleName);

            return Task.FromResult<Role>(result);
        }

        /// <summary>
        /// Updates role in asynchronous mode.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>Task result.</returns>
        /// <exception cref="System.ArgumentNullException">Role is null.</exception>
        public Task UpdateAsync(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("user");
            }

            _rolesRepository.Update(role);

            return Task.FromResult<Object>(null);
        }
    }
}
