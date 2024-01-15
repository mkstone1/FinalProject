using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectBackend.Features.Users.Model
{
    public class User
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("email")]
        public string Email;

        [JsonProperty("password")]
        public string Password;

        [JsonProperty("usernmae")]
        public string UserName;
    }
}
