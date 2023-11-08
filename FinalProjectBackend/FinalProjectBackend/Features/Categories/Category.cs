using Newtonsoft.Json;


namespace FinalProjectBackend.Features.Categories
{
    public class Category
    {
        [JsonProperty("id")]
        public string id;

        [JsonProperty("title")]
        public string Title;
    }
}
