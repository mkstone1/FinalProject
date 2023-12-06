using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendDataAccess.Models.Cards.Infrastructure;
using BackendDataAccess.Models.Categories.Infrastructure;
using BackendDataAccess.Models.Games.Infrastructure;
using BackendDataAccess.Services.Cards;
using BackendDataAccess.Services.Games;

[assembly: FunctionsStartup(typeof(FinalProjectBackend.Startup))]

namespace FinalProjectBackend
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<ICardRepository, CardRepository>();
            builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();
            builder.Services.AddSingleton<IGamesRepository, GamesRepository>();

            builder.Services.AddSingleton<ICardServices, CardServices>();
            builder.Services.AddSingleton<IGameServices, GameServices>();
        }
    }
}
