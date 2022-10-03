using Newtonsoft.Json;

namespace BasketAPI.Models.Details
{
    public class ArticleDetails
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonProperty("article")]
        public string? ArticleName { get; set; }

        [JsonProperty("price")]
        public decimal? Price { get; set; }

        [JsonIgnore]
        public int BasketId { get; set; }
    }
}
