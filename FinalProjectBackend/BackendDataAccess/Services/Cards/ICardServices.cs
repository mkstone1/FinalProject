using BackendDataAccess.Models.Cards.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendDataAccess.Services.Cards
{
    public interface ICardServices
    {
        Task<IEnumerable<Card>> GetCardsFromQueryString(IQueryCollection query);
        Task<IEnumerable<Card>> GetRandomCards();

    }
}
