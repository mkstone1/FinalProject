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
    }
}
