using System;
using Foo.Services.Common.Enums;

namespace Foo.Services.Common.Models
{
    /// <summary>
    /// The User model.
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// The Unique Identifier.
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// The Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Rating
        /// </summary>
        public int Rating { get; set; }

        
        /// <summary>
        /// The User status.
        /// </summary>
        public UserStatus Status { get; set; }
    }
}
