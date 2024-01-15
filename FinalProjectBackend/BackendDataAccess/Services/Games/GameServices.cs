using BackendDataAccess.Models.Games.Infrastructure;
using BackendDataAccess.Models.Games.Model;


namespace BackendDataAccess.Services.Games
{
    public class GameServices : IGameServices
    {
        private readonly IGamesRepository _gameRepository;

        public GameServices(IGamesRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<string> InitGame(Game game)
        {
            game.Id = Guid.NewGuid().ToString();
            game.TeamScore = new List<TeamScore>();
            game.TeamScore.Add(new TeamScore
            {
                teamName = "Hold 1",
                Score = 0,
                RoundsPlayed = 0
            });
            game.TeamScore.Add(new TeamScore
            {
                teamName = "Hold 2",
                Score = 0,
                RoundsPlayed = 0
            });

            game.CurrentTeam = "Hold 1";

            game.CreatedAt = DateTime.Now;

            await _gameRepository.UpsertGame(game);

            return game.Id;
        }

        public async Task<string> InitQuickStartGame(bool isRandom)
        {
            Game game = new Game();
            game.Id = Guid.NewGuid().ToString();
            game.TeamScore = new List<TeamScore>();
            game.TeamScore.Add(new TeamScore
            {
                teamName = "Hold 1",
                Score = 0,
                RoundsPlayed = 0
            });
            game.TeamScore.Add(new TeamScore
            {
                teamName = "Hold 2",
                Score = 0,
                RoundsPlayed = 0
            });

            game.CurrentTeam = "Hold 1";

            game.CreatedAt = DateTime.Now;
            game.MaxScore = 20;
            game.RoundLength = 60;

            game.WithRandomCards = isRandom;

            await _gameRepository.UpsertGame(game);

            return game.Id;
        }
    }
}
