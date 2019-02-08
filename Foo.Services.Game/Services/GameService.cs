using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foo.Services.Common.Enums;
using Foo.Services.Common.Interfaces;
using Foo.Services.Common.Models;
using Foo.Services.Player.Services;

namespace Foo.Services.Game.Services
{
    /// <summary>
    /// The Game Service
    /// </summary>
    public class GameService : IGameService
    {

        private readonly CloudTableStorageService _storageService;

        private readonly UserService _userService;

        /// <summary>
        /// The Game Service Constructor.
        /// </summary>
        public GameService()
        {
            this._storageService = new CloudTableStorageService();
            this._userService = new UserService();;
        }
        
        /// <summary>
        /// Add a new game async.
        /// </summary>
        /// <param name="userId">The first player</param>
        /// <returns>A new Game</returns>
        public async Task<GameModel> AddGameAsync(Guid userId)
        {
            var user = await _userService.GetUserAsync(userId);

            var game = new GameModel()
            {
                Id= Guid.NewGuid(),
                Rating = user.Rating,
                Score = new Tuple<int, int>(0,0),
                Status = GameStatus.LookingForPlayers,
                TeamOne = new Tuple<UserModel, UserModel>(user, null),
                TeamTwo = new Tuple<UserModel, UserModel>(null, null)
            };

            try
            {
                await _storageService.AddEntityAsync(game);

                return game;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            
        }

        public async Task<GameModel> GetGameAsync(Guid id)
        {
            try
            {
                return await _storageService.GetEntityAsync(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<GameModel> UpdateGameStatusAsync(GameStatus status, Guid id)
        {
            try
            {
                var game = await GetGameAsync(id);

                game.Status = status;

                await _storageService.UpdateGameAsync(game);

                return game;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<GameModel> AddPlayerToGame(Guid userId)
        {
            try
            {
                //get all games looking for players
                var user = await _userService.GetUserAsync(userId);

                var openGames = await _storageService.GetGamesListByStatus(GameStatus.LookingForPlayers);

                var game = openGames.OrderBy(x => Math.Abs(user.Rating - x.Rating)).FirstOrDefault();

                if (game?.TeamOne.Item2 == null)
                {
                    if (game != null) game.TeamOne = new Tuple<UserModel, UserModel>(game.TeamOne.Item1,user);
                } else if (game.TeamTwo.Item1 == null)
                {
                    game.TeamTwo = new Tuple<UserModel, UserModel>(user, null);
                }
                else if (game.TeamTwo.Item2 == null)
                {
                    game.TeamTwo = new Tuple<UserModel, UserModel>(game.TeamTwo.Item1, user);
                }

                throw new Exception("No available games");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
