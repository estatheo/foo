using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Foo.Services.Common.Enums;
using Foo.Services.Common.Models;

namespace Foo.Services.Common.Interfaces
{
    /// <summary>
    /// The Game Service Interface.
    /// </summary>
    public interface IGameService
    {
        /// <summary>
        /// Add new Game method.
        /// </summary>
        /// <param name="userId">The Player one id</param>
        /// <returns>A new Game</returns>
        Task<GameModel> AddGameAsync(Guid userId);

        /// <summary>
        /// Get User By Id.
        /// </summary>
        /// <param name="id">The game Id</param>
        /// <returns>The Game</returns>
        Task<GameModel> GetGameAsync(Guid id);

        /// <summary>
        /// Update the game status.
        /// </summary>
        /// <param name="status">The new game status</param>
        /// <param name="id">The game id</param>
        /// <returns>The Game</returns>
        Task<GameModel> UpdateGameStatusAsync(GameStatus status, Guid id);

        /// <summary>
        /// Add a player to a game.
        /// </summary>
        /// <param name="userId">The player id</param>
        /// <returns>The Game</returns>
        Task<GameModel> AddPlayerToGame(Guid userId);
    }
}
