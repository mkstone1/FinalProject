using FinalProjectBackend.Features.Cards;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendDataAccess.Models.Cards.Model
{
    public class Card
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("categoryId")]
        public string CategoryId;

        [JsonProperty("title")]
        public string Title;

        [JsonProperty("wrongAnswer")]
        public string WrongAnswer;

        [JsonProperty("answers")]
        public List<Answer> Answers;
    }
}
