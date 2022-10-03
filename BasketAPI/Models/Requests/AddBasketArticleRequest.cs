using Newtonsoft.Json;

namespace BasketAPI.Models.Requests
{
    public class AddBasketArticleRequest
    {
        [JsonProperty("article")]
        public string? ArticleName { get; set; }

        [JsonProperty("price")]
        public decimal? Price { get; set; } 
    }
}
