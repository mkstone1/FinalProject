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
using BackendDataAccess.Models.Cards.Model;
using BackendDataAccess.Models.Categories.Infrastructure;
using BackendDataAccess.Models.Categories.Model;

namespace FinalProjectBackend.Features.Categories
{
    public class CategoriesEndpoints
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesEndpoints(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [FunctionName("GetCategories")] 
        public async Task<IActionResult> GetCategories(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = CategoriesConstants.DefaultEndpoint)] HttpRequest req,
            ILogger log)
        {
            var categories = await _categoryRepository.GetAllCategories();
            return new OkObjectResult(categories);
        }


        [FunctionName("PostCategory")]
        public async Task<IActionResult> PostCategories(
           [HttpTrigger(AuthorizationLevel.Function, "post", Route = CategoriesConstants.DefaultEndpoint)] HttpRequest req,
           ILogger log)
        {

            log.LogInformation("Received a POST request.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<Category>(requestBody);

                await _categoryRepository.UpsertCategory(data);
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
