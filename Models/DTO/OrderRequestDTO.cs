using Newtonsoft.Json;

namespace API.Models.DTO
{
    public class OrderRequestDTO
    {
        [JsonProperty("productBalanceId")]
        public int ProductBalanceId { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
