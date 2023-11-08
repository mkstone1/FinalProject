using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using FinalProjectBackend.Features.Cards.Models;


namespace FinalProjectBackend.Features.Categories
{
    public class CategoriesEndpoints
    {

        [FunctionName("GetCategories")]
        public static async Task<IActionResult> GetCategories(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = CategoriesConstants.DefaultEndpoint)] HttpRequest req,
           [CosmosDB(databaseName:"hint-db", containerName: CategoriesConstants.Container, SqlQuery ="select * from c", Connection = "CosmosDBConnectionString" )] IEnumerable<Category> categories,
           ILogger log)
        {
       
            return new OkObjectResult(categories);
        }


        [FunctionName("PostCategory")]
        public static async Task<IActionResult> PostCategories(
           [HttpTrigger(AuthorizationLevel.Function, "post", Route = CategoriesConstants.DefaultEndpoint)] HttpRequest req,
           [CosmosDB(databaseName: "hint-db", containerName: CategoriesConstants.Container, Connection = "CosmosDBConnectionString")] IAsyncCollector<Category> category,
           ILogger log)
        {

            log.LogInformation("Received a POST request.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<Category>(requestBody);

                await category.AddAsync(data);
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
