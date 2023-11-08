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
using FinalProjectBackend.Features.Cards.Models;

namespace FinalProjectBackend.Features.Cards
{
    public static class CardEndpoints
    {
        [FunctionName("GetCards")]
        public static async Task<IActionResult> GetCards(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = CardConstants.DefaultEndpoint)] HttpRequest req,
           [CosmosDB(databaseName: "hint-db", containerName: CardConstants.Container, SqlQuery = "select * from c", Connection = "CosmosDBConnectionString")] IEnumerable<Card> cards,
           ILogger log)
        {
            log.LogInformation("Received a GET request for cards.");
            return new OkObjectResult(cards);
        }


        [FunctionName("GetCardsFromCategory")]
        public static async Task<IActionResult> GetCardsFromCategory(
      [HttpTrigger(AuthorizationLevel.Function, "get", Route = CardConstants.GetCardsFromCategory)] HttpRequest req,
      [CosmosDB(databaseName: "hint-db", containerName: CardConstants.Container, SqlQuery = "select * from c WHERE c.id = {categoryId}", Connection = "CosmosDBConnectionString")] IEnumerable<Card> cards,
      ILogger log)
        {
            log.LogInformation("Received a GET request for cards.");
            string categoryId = req.Query["category-id"];

            if (string.IsNullOrEmpty(categoryId))
            {
                return new BadRequestObjectResult("Please provide a valid ID in the query string.");
            }

            return new OkObjectResult(cards);
        }

        [FunctionName("PostCard")]
        public static async Task<IActionResult> PostCard(
         [HttpTrigger(AuthorizationLevel.Function, "post", Route = CardConstants.DefaultEndpoint)] HttpRequest req,
         [CosmosDB(databaseName: "hint-db", containerName: CardConstants.Container, Connection = "CosmosDBConnectionString")] IAsyncCollector<Card> card,
         ILogger log)
        {
            log.LogInformation("Received a POST request.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<Card>(requestBody);

                data.Id = Guid.NewGuid().ToString();

                await card.AddAsync(data);
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
