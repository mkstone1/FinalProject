using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using BackendDataAccess.Models.Cards.Model;
using Microsoft.Azure.Cosmos;
using BackendDataAccess.Models.Cards.Infrastructure;
using Microsoft.Azure.Cosmos.Linq;
using BackendDataAccess.Services.Cards;

namespace FinalProjectBackend.Features.Cards
{
    public class CardEndpoints
    {
        private readonly ICardRepository _cardRepository;
        private readonly ICardServices _cardServices;

        public CardEndpoints(ICardRepository cardRepository, ICardServices cardServices)
        {
            _cardRepository = cardRepository;
            _cardServices = cardServices;
        }

        [FunctionName("GetCards")]
        public  async Task<IActionResult> GetCards(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = CardConstants.DefaultEndpoint)] HttpRequest req,
           ILogger log)
        {
            log.LogInformation("Received a GET request for cards.");

            if (req.Query.Count != 0)
            {
                try { 
                var cards = await _cardServices.GetCardsFromQueryString(req.Query);
                return new OkObjectResult(cards);
                }
                catch(Exception ex)
                {
                    return new BadRequestObjectResult(ex.Message);
                }
            }
            else
            {
                var cards = await _cardRepository.GetAllCardsAsync();
                return new OkObjectResult(cards);
            }     
        }


        [FunctionName("PostCard")]
        public  async Task<IActionResult> PostCard(
         [HttpTrigger(AuthorizationLevel.Function, "post", Route = CardConstants.DefaultEndpoint)] HttpRequest req,
         ILogger log)
        {
            log.LogInformation("Received a POST request.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<Card>(requestBody);

                await _cardRepository.UpsertCard(data);
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
