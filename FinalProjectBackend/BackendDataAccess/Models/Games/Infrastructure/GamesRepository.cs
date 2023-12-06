using BackendDataAccess.Models.Cards.Model;
using BackendDataAccess.Models.Categories.Model;
using BackendDataAccess.Models.Games.Model;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendDataAccess.Models.Games.Infrastructure
{
    public class GamesRepository : IGamesRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public GamesRepository()
        {
            _cosmosClient = new CosmosClient(Environment.GetEnvironmentVariable("CosmosDBConnectionString"));
            _container = _cosmosClient.GetContainer("hint-db", "Games");
        }

        public async Task<IEnumerable<Game>> GetAllGames()
        {
            var query = new QueryDefinition("Select * from c");
            var iterator = _container.GetItemQueryIterator<Game>(query);

            var results = new List<Game>();

            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                results.AddRange(response);
            }

            return results;
        }

        public async Task<Game> GetGameFromId(string gameId)
        {
            var query = new QueryDefinition("SELECT * FROM c WHERE c.id = @gameId")
            .WithParameter("@gameId", gameId);

            var iterator = _container.GetItemQueryIterator<Game>(query);

            var cards = new List<Game>();

            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                cards.AddRange(response);
            }

            return cards.First();
        }

        public async Task UpsertGame(Game game)
        {
            try
            {
                await _container.CreateItemAsync(game);
            }
            catch (Exception ex)
            {
                throw new Exception($"An unexpected error occurred: {ex.Message}", ex);
            }

        }
    }
}
