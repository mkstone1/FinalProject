using BackendDataAccess.Models.Categories.Model;
using BackendDataAccess.Models.Games.Infrastructure;
using BackendDataAccess.Models.Games.Model;
using FinalProjectBackend.Features.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;

using System.Threading.Tasks;

namespace FinalProjectBackend.Features.Games
{
    public class GamesEndpoints
    {
        private readonly IGamesRepository _gamesRepository;
        public GamesEndpoints(IGamesRepository gamesRepository)
        {
            _gamesRepository = gamesRepository;
        }


        [FunctionName("GetGames")]
        public async Task<IActionResult> GetCategories(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = GamesConstants.DefaultEndpoint)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Received a GET request for cards.");

            if (req.Query.ContainsKey("gameId"))
            {
                string gameId = req.Query["gameId"];

                // Return cards filtered by category
                var game = await _gamesRepository.GetGameFromId(gameId);
                return new OkObjectResult(game);

            }
            else
            {
                var games = await _gamesRepository.GetAllGames();
                return new OkObjectResult(games);
            }
        }


        [FunctionName("PostGame")]
        public async Task<IActionResult> PostCategories(
      [HttpTrigger(AuthorizationLevel.Function, "post", Route = GamesConstants.DefaultEndpoint)] HttpRequest req,
      ILogger log)
        {

            log.LogInformation("Received a POST request.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<Game>(requestBody);

                await _gamesRepository.UpsertGame(data);
                return new OkObjectResult("Data saved to Cosmos DB.");
            }
            catch (Exception ex)
            {
                log.LogError($"Error: {ex.Message}");
                return new BadRequestObjectResult("Failed to save data to Cosmos DB.");
            }

        }
    }
}
