using System;
using System.IO;
using System.Threading.Tasks;
using Foo.Services.Common.Enums;
using Foo.Services.Game.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Foo.Services.Game
{
    public static class Game
    {
        [FunctionName("Game")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        { 

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<InputModel>(requestBody);
            switch (input.Action)
            {
                case GameAction.CreateGame:
                {
                    break;
                }
                case GameAction.EndGame:
                {
                    break;
                }
                case GameAction.LookForGame:
                {
                    break;
                }
                case GameAction.UpdateGame:
                {
                    break;
                }
                case GameAction.StartGame:
                {
                    break;
                }
            }

            return
                new BadRequestObjectResult("Please pass a game action type.");
        }
    }
}
