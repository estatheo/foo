using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace Foo.Services.Game.Entities
{
    /// <summary>
    /// The User Entity Model.
    /// </summary>
    public class GameEntity : TableEntity
    {
        /// <summary>
        /// The Game Entity Constructor.
        /// </summary>
        /// <param name="sKey">The partition key</param>
        /// <param name="sRow">The Row key</param>
        public GameEntity(string sKey, string sRow)
        {
            this.PartitionKey = sKey;
            this.RowKey = sRow;
        }

        /// <summary>
        /// The Game Entity Constructor.
        /// </summary>
        public GameEntity() { }

        /// <summary>
        /// The Game Team 1 - Player 1.
        /// </summary>
        public string TeamOnePlayerOne { get; set; }


        /// <summary>
        /// The Game Team 1 - Player 2.
        /// </summary>
        public string TeamOnePlayerTwo { get; set; }

        /// <summary>
        /// The Game Team 2 - Player 1.
        /// </summary>
        public string TeamTwoPlayerOne { get; set; }

        /// <summary>
        /// The Game Team 2 - Player 2.
        /// </summary>
        public string TeamTwoPlayerTwo { get; set; }
        
        /// <summary>
        /// The Game Score.
        /// </summary>
        public Tuple<int,int> Score { get; set; }

        /// <summary>
        /// The Game Rating.
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// The Game status.
        /// </summary>
        public int Status { get; set; }
    }
}
