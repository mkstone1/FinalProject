using BackendDataAccess.Models.Categories.Model;
using BackendDataAccess.Models.Games.Infrastructure;
using BackendDataAccess.Models.Games.Model;
using BackendDataAccess.Services.Games;
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
        private readonly IGamesRepository _gameRepository;
        private readonly IGameServices _gameServices;
        public GamesEndpoints(IGamesRepository gamesRepository, IGameServices gameServices)
        {
            _gameRepository = gamesRepository;
            _gameServices = gameServices;
        }


        [FunctionName("GetGames")]
        public async Task<IActionResult> GetGame(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = GamesConstants.DefaultEndpoint)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Received a GET request for Game.");

            if (req.Query.ContainsKey("gameId"))
            {
                string gameId = req.Query["gameId"];
                var game = await _gameRepository.GetGameFromId(gameId);
                return new OkObjectResult(game);
            }
            else
            {
                var games = await _gameRepository.GetAllGames();
                return new OkObjectResult(games);
            }
        }


        [FunctionName("PostGame")]
        public async Task<IActionResult> PostGame(
      [HttpTrigger(AuthorizationLevel.Function, "post", Route = GamesConstants.DefaultEndpoint)] HttpRequest req,
      ILogger log)
        {
            log.LogInformation("Received a POST request for Game.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<Game>(requestBody);
                if (req.Query.Count != 0)
                {
                    return new OkObjectResult(data);
                }
                else
                {
                    var gameId = await _gameServices.InitGame(data);
                    return new OkObjectResult(gameId);
                }
            }
            catch (Exception ex)
            {
                log.LogError($"Error: {ex.Message}");
                return new BadRequestObjectResult("Failed to save data to Cosmos DB.");
            }
        }

        [FunctionName("PostQuickStartGame")]
        public async Task<IActionResult> PostQuickStartGame(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = GamesConstants.QuickStartEndpoint)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Received a POST request for QuickStartGame.");

            try
            {
                var gameId = await _gameServices.InitQuickStartGame();
                return new JsonResult(gameId);
            }
            catch (Exception ex)
            {
                log.LogError($"Error: {ex.Message}");
                return new BadRequestObjectResult("Failed to save data to Cosmos DB.");
            }
        }

        [FunctionName("PutGame")]
        public async Task<IActionResult> PutGame(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = GamesConstants.DefaultEndpoint)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Received a Put request for QuickStartGame.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<Game>(requestBody);

                await _gameRepository.UpsertGame(data);

                return new OkResult();
                  
            }
            catch (Exception ex)
            {
                log.LogError($"Error: {ex.Message}");
                return new BadRequestObjectResult("Failed to save data to Cosmos DB.");
            }
        }
    }
}
