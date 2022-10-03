using Newtonsoft.Json;

namespace BasketAPI.Models.Details
{
    public class BasketDetails
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("articles")]
        public List<Article> Articles { get; set; }

        [JsonProperty("totalNet")]
        public decimal TotalNet { get => Articles.Sum(p => p.Price); } 

        [JsonProperty("totalGross")]
        public decimal TotalGross { get; set; }

        [JsonProperty("customer")]
        public string? Customer { get; set; }

        [JsonProperty("paysVAT")]
        public bool PaysVAT { get; set; }

        [JsonIgnore]
        public string? Status { get; set; } = "Opened";
    }
}
