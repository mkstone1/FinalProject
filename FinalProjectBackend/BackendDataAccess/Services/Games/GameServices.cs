﻿using BackendDataAccess.Models.Games.Infrastructure;
using BackendDataAccess.Models.Games.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            for(int i=0 ; i< game.AmountOfTeams; i++)
            {
                game.TeamScore.Add(new TeamScore
                {
                    teamName = $"team{i}",
                    Score = 0,
                    RoundsPlayed = 0
                }); ;
            }
            game.CreatedAt = DateTime.Now;
            await _gameRepository.UpsertGame(game);

            return game.Id;
        }
    }
}