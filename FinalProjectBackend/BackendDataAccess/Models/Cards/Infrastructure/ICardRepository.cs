using BackendDataAccess.Models.Cards.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendDataAccess.Models.Cards.Infrastructure
{
    public interface ICardRepository
    {
        Task<IEnumerable<Card>> GetAllCardsAsync();

        Task<IEnumerable<Card>> GetCardFromCardId(string cardId);

        Task<IEnumerable<Card>> GetCardsByCategoryAsync(string categoryId);

        Task UpsertCard(Card card);

        
    }
}
