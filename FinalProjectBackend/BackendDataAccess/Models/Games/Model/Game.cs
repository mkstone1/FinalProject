using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendDataAccess.Models.Games.Model
{
    public class Game
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("roundLength")]
        public int RoundLength;

        [JsonProperty("numberOfRounds")]
        public int NumberOfRounds;

        [JsonProperty("currentRound")]
        public int CurrentRound;

        [JsonProperty("amountOfTeams")]
        public int AmountOfTeams;

        [JsonProperty("teamScore")]
        public List<TeamScore> TeamScore;

        [JsonProperty("currentTeam")]
        public string CurrentTeam;

        [JsonProperty("maxScore")]
        public int MaxScore;

        public DateTime CreatedAt;
    }
}
