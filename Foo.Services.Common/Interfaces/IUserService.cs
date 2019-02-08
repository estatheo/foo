using System;
using System.Threading.Tasks;
using Foo.Services.Common.Models;

namespace Foo.Services.Common.Interfaces
{
    /// <summary>
    /// The User Service Interface.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Add User method.
        /// </summary>
        /// <param name="name">User name</param>
        /// <returns>A new User</returns>
        Task<UserModel> AddUserAsync(string name);

        /// <summary>
        /// Get User By Id.
        /// </summary>
        /// <param name="id">The user Id</param>
        /// <returns>The User</returns>
        Task<UserModel> GetUserAsync(Guid id);

        /// <summary>
        /// Get User By Name.
        /// </summary>
        /// <param name="name">The User name</param>
        /// <returns>The User</returns>
        Task<UserModel> GetUserAsync(string name);
    }
}
