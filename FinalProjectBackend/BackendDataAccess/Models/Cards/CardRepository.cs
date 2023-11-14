using BackendDataAccess.Models.Cards.Model;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Container = Microsoft.Azure.Cosmos.Container;

namespace BackendDataAccess.Models.Cards
{
    public class CardRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Database _database;
        private readonly Container _container;

        public CardRepository(CosmosClient cosmosClient, Database database, Container container)
        {
            _cosmosClient = cosmosClient;
            _database = database;
            _container = container;
        }

        public async Task<IEnumerable<Card>> GetAllCardsAsync()
        {
            var query = new QueryDefinition("Select * from c");
            var iterator = _container

        }

    }
}
