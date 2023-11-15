using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Extensions.Http;
using FinalProjectBackend.Features.Users.Model;

namespace FinalProjectBackend.Features.Users
{
    public class UserEndpoints
    {

        [FunctionName("GetUsers")]
        public static async Task<IActionResult> GetCards(
         [HttpTrigger(AuthorizationLevel.Function, "get", Route = UserConstants.DefaultEndpoint)] HttpRequest req,
         [CosmosDB(databaseName: "hint-db", containerName: UserConstants.Container, SqlQuery = "select * from c", Connection = "CosmosDBConnectionString")] IEnumerable<User> users,
         ILogger log)
        {
            log.LogInformation("Received a GET request for cards.");
            return new OkObjectResult(users);
        }


        [FunctionName("PostUser")]
        public static async Task<IActionResult> PostUser(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = UserConstants.DefaultEndpoint)] HttpRequest req,
            [CosmosDB(databaseName: "hint-db", containerName: UserConstants.Container, Connection = "CosmosDBConnectionString")] IAsyncCollector<User> card,
            ILogger log)
        {
            log.LogInformation("Received a POST request.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<User>(requestBody);

                data.Id = Guid.NewGuid().ToString();
                data.Password = BCrypt.Net.BCrypt.HashPassword(data.Password);

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
