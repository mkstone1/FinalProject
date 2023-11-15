using BackendDataAccess.Models.Cards.Model;
using BackendDataAccess.Models.Categories.Model;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendDataAccess.Models.Categories.Infrastructure
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public CategoryRepository()
        {
            _cosmosClient = new CosmosClient(Environment.GetEnvironmentVariable("CosmosDBConnectionString"));
            _container = _cosmosClient.GetContainer("hint-db", "Categories");
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var query = new QueryDefinition("Select * from c");
            var iterator = _container.GetItemQueryIterator<Category>(query);

            var results = new List<Category>();

            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                results.AddRange(response);
            }

            return results;
        }

        public async Task UpsertCategory(Category category)
        {
            try
            {
                await _container.CreateItemAsync(category);
            }
            catch (Exception ex)
            {
                throw new Exception($"An unexpected error occurred: {ex.Message}", ex);
            }

        }
    }
}
