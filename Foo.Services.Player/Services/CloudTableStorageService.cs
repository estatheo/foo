using System;
using System.Linq;
using System.Threading.Tasks;
using Foo.Services.Common.Enums;
using Foo.Services.Common.Interfaces;
using Foo.Services.Common.Models;
using Foo.Services.User.Entities;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Foo.Services.User.Services
{
    /// <summary>
    /// The Azure Cloud Table Storage Service.
    /// </summary>
    public class CloudTableStorageService : IStorageService<UserModel>
    {


        /// <summary>
        /// The Cloud Table Storage Service Constructor.
        /// </summary>
        public CloudTableStorageService()
        {

        }


        /// <summary>
        /// Add User method.
        /// </summary>
        /// <param name="entity">The user</param>
        public async Task AddEntityAsync(UserModel entity)
        {
            var userEntity = new UserEntity(Environment.GetEnvironmentVariable("StorageAccount:UserTable:Key"), entity.Id.ToString())
            {
                Name = entity.Name,
                Rating = entity.Rating,
                Status = (int)entity.Status
            };

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("StorageAccount:ConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference(Environment.GetEnvironmentVariable("StorageAccount:UserTable:Name"));

            await table.CreateIfNotExistsAsync();

            TableOperation insertOperation = TableOperation.Insert(userEntity);

            await table.ExecuteAsync(insertOperation);

            //todo: add error check
        }

        /// <summary>
        /// Get User By Id.
        /// </summary>
        /// <param name="id">The user Id</param>
        /// <returns>The User</returns>
        public async Task<UserModel> GetEntityAsync(Guid id)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("StorageAccount:ConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference(Environment.GetEnvironmentVariable("StorageAccount:UserTable:Name"));

            await table.CreateIfNotExistsAsync();
            
            TableOperation retrieveOperation = TableOperation.Retrieve<UserEntity>(Environment.GetEnvironmentVariable("StorageAccount:UserTable:Key"), $"{id}");

            var result = await table.ExecuteAsync(retrieveOperation);

            if (result.Result is UserEntity userResult)
            {
                return new UserModel
                {
                    Id = Guid.Parse(userResult.RowKey),
                    Name = userResult.Name,
                    Rating = userResult.Rating,
                    Status = (UserStatus)userResult.Status
                };
            }
             throw new Exception("No user Found");
        }

        /// <summary>
        /// Get User By Name.
        /// </summary>
        /// <param name="name">The User name</param>
        /// <returns>The User</returns>
        public async Task<UserModel> GetUserAsync(string name)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("StorageAccount:ConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference(Environment.GetEnvironmentVariable("StorageAccount:UserTable:Name"));

            await table.CreateIfNotExistsAsync();

            TableQuery<UserEntity> retrieveOperation =
                new TableQuery<UserEntity>().Where(
                    TableQuery.GenerateFilterCondition("Name", QueryComparisons.Equal, name));

            var result = await table.ExecuteQuerySegmentedAsync(retrieveOperation, new TableContinuationToken());

            if (result.Results.FirstOrDefault() is UserEntity userResult)
            {
                return new UserModel
                {
                    Id = Guid.Parse(userResult.RowKey),
                    Name = userResult.Name,
                    Rating = userResult.Rating,
                    Status = (UserStatus)userResult.Status
                };
            }
            throw new Exception("No user Found");
        }
    }
}
