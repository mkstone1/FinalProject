using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendDataAccess.Models.Games.Model
{
    public class TeamScore
    {
        [JsonProperty("name")]
        public string teamName;

        [JsonProperty("score")]
        public int Score;

        [JsonProperty("roundsPlayed")]
        public int RoundsPlayed;

    }
}
