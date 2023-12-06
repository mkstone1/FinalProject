using BackendDataAccess.Models.Cards.Model;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Container = Microsoft.Azure.Cosmos.Container;

namespace BackendDataAccess.Models.Cards.Infrastructure
{
    public class CardRepository : ICardRepository
    {
        private readonly CosmosClient _cosmosClient;
    
        private readonly Container _container;

        public CardRepository()
        {
            _cosmosClient = new CosmosClient(Environment.GetEnvironmentVariable("CosmosDBConnectionString"));
      
            _container = _cosmosClient.GetContainer("hint-db", "Cards");
        }

        public async Task<IEnumerable<Card>> GetAllCardsAsync()
        {
            var query = new QueryDefinition("Select * from c");
            var iterator = _container.GetItemQueryIterator<Card>(query);

            var results = new List<Card>();

            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                results.AddRange(response);
            }

            return results;

        }

        public async Task<IEnumerable<Card>> GetCardsByCategoryAsync(string categoryId)
        {
            var query = new QueryDefinition("SELECT * FROM c WHERE c.categoryId = @categoryId")
            .WithParameter("@categoryId", categoryId);

            var iterator = _container.GetItemQueryIterator<Card>(query);

            var cards = new List<Card>();

            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                cards.AddRange(response);
            }

            return cards;
        }

        public async Task<IEnumerable<Card>> GetCardFromCardId(string cardId)
        {
            var query = new QueryDefinition("SELECT * FROM c WHERE c.id = @cardId")
            .WithParameter("@cardId", cardId);

            var iterator = _container.GetItemQueryIterator<Card>(query);

            var cards = new List<Card>();

            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                cards.AddRange(response);
            }

            return cards;
        }

        public async Task UpsertCard(Card card)
        {
            card.Id = Guid.NewGuid().ToString();

            try
            {
                await _container.CreateItemAsync(card);
            }
            catch (Exception ex)
            {
                throw new Exception($"An unexpected error occurred: {ex.Message}", ex);
            }

        }

    }
}
