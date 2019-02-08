using System;
using System.IO;
using System.Threading.Tasks;
using Foo.Services.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Foo.Services.User.Models;
using Foo.Services.User.Services;

namespace Foo.Services.Player
{
    /// <summary>
    /// The User Function
    /// </summary>
    public static class User
    {
        /// <summary>
        /// The User Function, Run method.
        /// </summary>
        /// <param name="req">The incoming request</param>
        /// <param name="log">The logger</param>
        /// <returns></returns>
        [FunctionName("User")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<InputModel>(requestBody);

            UserModel user = null;
            UserService userService = new UserService();
            if (input.Id != Guid.Empty)
            {
                user = await userService.GetUserAsync(input.Id);

                return new OkObjectResult(user);
            }
            else if (input.Name != null && !string.IsNullOrWhiteSpace(input.Name))
            {
                user = await userService.AddUserAsync(input.Name);

                return new OkObjectResult(user.Id);
            }

           
            return new BadRequestObjectResult("Please provide the id or the name of the player.");

        }
    }
}
e player.");

        }
    }
}
