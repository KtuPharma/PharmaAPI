using System.Collections.Generic;
using Newtonsoft.Json;

namespace API.Models.DTO
{
    public class GetOrdersDTO<T>
    {
        [JsonProperty("meta")] public Meta Meta { get; set; }
        [JsonProperty("data")] public IEnumerable<OrdersDTO<T>> Data { get; set; }

        public GetOrdersDTO(IEnumerable<OrdersDTO<T>> orders)
        {
            Meta = new Meta();
            Data = orders;
        }
    }
}
