using BackendDataAccess.Models.Games.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendDataAccess.Services.Games
{
    public interface IGameServices
    {
        Task<string> InitGame(Game game);
    }
}
