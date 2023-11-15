using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectBackend.Features.Cards
{
    public class Answer
    {
        [JsonProperty("id")]
        public int id;
        [JsonProperty("answer")]
        public string answer;
    }
}
