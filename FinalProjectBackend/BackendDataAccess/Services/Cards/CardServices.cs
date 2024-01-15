using BackendDataAccess.Models.Cards.Infrastructure;
using BackendDataAccess.Models.Cards.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendDataAccess.Services.Cards
{
    public class CardServices : ICardServices
    {
        private readonly ICardRepository _cardRepository;

        public CardServices(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<IEnumerable<Card>> GetCardsFromQueryString(IQueryCollection queryString)
        {


            if (queryString.ContainsKey("categoryId"))
            {
                string categoryId = queryString["categoryId"];
                var cardsByCategory = await _cardRepository.GetCardsByCategoryAsync(categoryId);
                return cardsByCategory;
            }
            else if (queryString.ContainsKey("cardId"))
            {
                string cardId = queryString["cardId"];
                var card = await _cardRepository.GetCardFromCardId(cardId);
                return card;
            }
            else
            {
                throw new Exception("Query string is not valid");
            }

        }

        public async Task<IEnumerable<Card>> GetRandomCards()
        {
            var cards = new List<Card>();
     
            for (int i =0; i <4; i++)
            {
                var category = SelectCategory();

                var card = await _cardRepository.GetRandomCardByCategory(category);
                cards.AddRange(card);
            }
       
            return cards;

        }

        private string SelectCategory()
        {
            Dictionary<string, double> categoryProbabilities = new Dictionary<string, double>
        {
            { "Nynne", 1 },
            { "Tale", 1 },
            { "Mime", 1 },
        };

            Random random = new Random();
       

            double totalProbability = categoryProbabilities.Values.Sum();

           
            double randomValue = random.NextDouble() * totalProbability;

      
            double cumulativeProbability = 0;

            foreach (var category in categoryProbabilities.Keys)
            {
                cumulativeProbability += categoryProbabilities[category];

                if (randomValue <= cumulativeProbability)
                {
                    return category;
                }
            }

            return categoryProbabilities.Keys.Last();
        }
    }
}
