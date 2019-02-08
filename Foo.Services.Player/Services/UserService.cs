using System;
using System.Threading.Tasks;
using Foo.Services.Common.Enums;
using Foo.Services.Common.Interfaces;
using Foo.Services.Common.Models;

namespace Foo.Services.User.Services
{
    /// <summary>
    /// The User Service
    /// </summary>
    public class UserService : IUserService
    {

        private readonly CloudTableStorageService _storageService;

        /// <summary>
        /// The User Service Constructor.
        /// </summary>
        public UserService()
        {
            this._storageService = new CloudTableStorageService();
        }

        /// <summary>
        /// Add User method.
        /// </summary>
        /// <param name="name">User name</param>
        /// <returns>A new User</returns>
        public async Task<UserModel> AddUserAsync(string name)
        {
            //todo: check for errors
            var user = new UserModel()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Rating = int.Parse(Environment.GetEnvironmentVariable("General:StartingRating")),
                Status = UserStatus.Idle
            };

            await _storageService.AddEntityAsync(user);

            return user;
        }

        /// <summary>
        /// Get User By Id.
        /// </summary>
        /// <param name="id">The user Id</param>
        /// <returns>The User</returns>
        public async Task<UserModel> GetUserAsync(Guid id)
        {
            //todo: check for errors
            return await _storageService.GetEntityAsync(id);
        }

        /// <summary>
        /// Get User By Name.
        /// </summary>
        /// <param name="name">The User name</param>
        /// <returns>The User</returns>
        public async Task<UserModel> GetUserAsync(string name)
        {
            //todo: check for errors
            return await _storageService.GetUserAsync(name);
        }
    }
}
