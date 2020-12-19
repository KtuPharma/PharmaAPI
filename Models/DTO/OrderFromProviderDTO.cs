using Newtonsoft.Json;

namespace API.Models.DTO
{
    public class OrderFromProviderDTO
    {
        [JsonProperty("productBalanceId")]
        public int ProductBalanceId { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
