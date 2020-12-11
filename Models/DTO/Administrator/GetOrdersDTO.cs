using System.Collections.Generic;
using Newtonsoft.Json;

namespace API.Models.DTO
{
    public class GetOrdersDTO
    {
        [JsonProperty("meta")] public Meta Meta { get; set; }
        [JsonProperty("data")] public IEnumerable<OrdersDTO> Data { get; set; }

        public GetOrdersDTO(IEnumerable<OrdersDTO> orders)
        {
            Meta = new Meta();
            Data = orders;
        }
    }
}
