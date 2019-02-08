using System;
using System.Collections.Generic;
using System.Text;
using Foo.Services.Common.Enums;

namespace Foo.Services.Common.Models
{
    /// <summary>
    /// The Game model.
    /// </summary>
    public class GameModel
    {
        /// <summary>
        /// The Game Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The Game Team 1.
        /// </summary>
        public Tuple<UserModel, UserModel> TeamOne { get; set; }

        /// <summary>
        /// The Game Team 2.
        /// </summary>
        public Tuple<UserModel, UserModel> TeamTwo { get; set; }

        /// <summary>
        /// The Game Score.
        /// </summary>
        public Tuple<int, int> Score { get; set; }

        /// <summary>
        /// The Game Rating.
        /// </summary>
        public int Rating { get; set; }


        /// <summary>
        /// The Game status.
        /// </summary>
        public GameStatus Status { get; set; }
    }
}
