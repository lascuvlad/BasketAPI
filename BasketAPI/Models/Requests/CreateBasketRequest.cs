using Newtonsoft.Json;

namespace BasketAPI.Models.Requests
{
    public class CreateBasketRequest
    {
        [JsonProperty("customer")]
        public string? Customer { get; set; }

        [JsonProperty("paysVAT")]
        public bool PaysVAT { get; set; } = false;
    }
}
