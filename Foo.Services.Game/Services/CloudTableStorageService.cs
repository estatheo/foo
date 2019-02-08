using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foo.Services.Common.Enums;
using Foo.Services.Common.Interfaces;
using Foo.Services.Common.Models;
using Foo.Services.Game.Entities;
using Foo.Services.Player.Services;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Foo.Services.Game.Services
{
    /// <summary>
    /// The Azure Cloud Table Storage Service.
    /// </summary>
    public class CloudTableStorageService : IStorageService<GameModel>
    {

        private readonly UserService _userService;

        /// <summary>
        /// The Cloud Table Storage Service Constructor.
        /// </summary>
        public CloudTableStorageService()
        {
            _userService = new UserService();
        }


        /// <summary>
        /// Add Game method.
        /// </summary>
        /// <param name="entity">The Game</param>
        public async Task AddEntityAsync(GameModel entity)
        {
            var gameEntity = new GameEntity(Environment.GetEnvironmentVariable("StorageAccount:GameTable:Key"), entity.Id.ToString())
            {
                 Rating = entity.Rating,
                 Score = entity.Score,
                 TeamOnePlayerOne = entity.TeamOne.Item1.Id.ToString()
            };

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("StorageAccount:ConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference(Environment.GetEnvironmentVariable("StorageAccount:GameTable:Name"));

            await table.CreateIfNotExistsAsync();

            TableOperation insertOperation = TableOperation.Insert(gameEntity);

            await table.ExecuteAsync(insertOperation);

            //todo: add error check
        }

        /// <summary>
        /// Get Game By Id.
        /// </summary>
        /// <param name="id">The game Id</param>
        /// <returns>The Game</returns>
        public async Task<GameModel> GetEntityAsync(Guid id)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("StorageAccount:ConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference(Environment.GetEnvironmentVariable("StorageAccount:GameTable:Name"));

            await table.CreateIfNotExistsAsync();
            
            TableOperation retrieveOperation = TableOperation.Retrieve<GameEntity>(Environment.GetEnvironmentVariable("StorageAccount:GameTable:Key"), $"{id}");

            var result = await table.ExecuteAsync(retrieveOperation);

            if (result.Result is GameEntity gameResult)
            {
                return new GameModel
                {
                    Id = Guid.Parse(gameResult.RowKey),
                    Rating = gameResult.Rating,
                    Status = (GameStatus)gameResult.Status,
                    Score = gameResult.Score,
                    TeamOne = new Tuple<UserModel, UserModel>(await _userService.GetUserAsync(Guid.Parse(gameResult.TeamOnePlayerOne)), await _userService.GetUserAsync(Guid.Parse(gameResult.TeamOnePlayerTwo))),
                    TeamTwo = new Tuple<UserModel, UserModel>(await _userService.GetUserAsync(Guid.Parse(gameResult.TeamTwoPlayerOne)), await _userService.GetUserAsync(Guid.Parse(gameResult.TeamTwoPlayerTwo)))
                };
            }
             throw new Exception("No game Found");
        }

        public async Task UpdateGameAsync(GameModel game)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("StorageAccount:ConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference(Environment.GetEnvironmentVariable("StorageAccount:GameTable:Name"));

            await table.CreateIfNotExistsAsync();

            var gameEntity = new GameEntity(Environment.GetEnvironmentVariable("StorageAccount:UserTable:Key"), game.Id.ToString())
            {
                Rating = game.Rating,
                Score = game.Score,
                Status = (int)game.Status,
                TeamOnePlayerOne = game.TeamOne.Item1.Id.ToString(),
                TeamOnePlayerTwo = game.TeamOne.Item2.Id.ToString(),
                TeamTwoPlayerOne = game.TeamTwo.Item1.Id.ToString(),
                TeamTwoPlayerTwo = game.TeamTwo.Item2.Id.ToString()
            };
            
            TableOperation replaceOperation = TableOperation.Replace(gameEntity);
        }

        public async Task<List<GameModel>> GetGamesListByStatus(GameStatus status)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("StorageAccount:ConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference(Environment.GetEnvironmentVariable("StorageAccount:GameTable:Name"));

            await table.CreateIfNotExistsAsync();

            TableQuery<GameEntity> retrieveOperation =
                new TableQuery<GameEntity>().Where(
                    TableQuery.GenerateFilterCondition("Status", QueryComparisons.Equal, ((int)status).ToString()));

            var result = await table.ExecuteQuerySegmentedAsync(retrieveOperation, new TableContinuationToken());
            
            List<GameModel> gameList = new List<GameModel>();
            foreach (var game in result.Results)
            {
                gameList.Add(new GameModel()
                {
                    Id = Guid.Parse(game.RowKey),
                    Rating = game.Rating,
                    Score = game.Score,
                    Status = (GameStatus)game.Status,
                    TeamOne = new Tuple<UserModel, UserModel>(await _userService.GetUserAsync(game.TeamOnePlayerOne), await _userService.GetUserAsync(game.TeamOnePlayerTwo)),
                    TeamTwo = new Tuple<UserModel, UserModel>(await _userService.GetUserAsync(game.TeamTwoPlayerOne), await _userService.GetUserAsync(game.TeamTwoPlayerTwo)),
                });
            }

            return gameList;

        }
    }
}
