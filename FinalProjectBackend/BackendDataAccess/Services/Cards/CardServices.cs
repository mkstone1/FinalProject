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
    }
}
