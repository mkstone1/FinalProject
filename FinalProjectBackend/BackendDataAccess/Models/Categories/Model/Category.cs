using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendDataAccess.Models.Categories.Model
{
    public class Category
    {
        [JsonProperty("id")]
        public string id;
    }
}
