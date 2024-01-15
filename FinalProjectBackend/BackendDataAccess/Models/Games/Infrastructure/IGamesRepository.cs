using BackendDataAccess.Models.Games.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendDataAccess.Models.Games.Infrastructure
{
    public interface IGamesRepository
    {
        Task<IEnumerable<Game>> GetAllGames();

        Task<Game> GetGameFromId(string gameId);
        Task UpsertGame(Game game);

        Task DeleteAllGames();
    }
}
