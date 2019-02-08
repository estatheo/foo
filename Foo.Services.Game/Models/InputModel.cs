using System;
using Foo.Services.Common.Enums;

namespace Foo.Services.Game.Models
{
    /// <summary>
    /// The User Function Input Model.
    /// </summary>
    public class InputModel
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The call action.
        /// </summary>
        public GameAction Action { get; set; }

        /// <summary>
        /// The player id.
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// The Final score.
        /// </summary>
        public Tuple<int,int> Score { get; set; }
    }
}
