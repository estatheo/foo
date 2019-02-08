using System;
using System.Collections.Generic;
using System.Text;

namespace Foo.Services.User.Models
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
        /// The user name.
        /// </summary>
        public string Name { get; set; }
    }
}
