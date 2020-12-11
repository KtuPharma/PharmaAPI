using System.Collections.Generic;
using Newtonsoft.Json;

namespace API.Models.DTO
{
    public class GetProductBalancesDTO
    {
        [JsonProperty("meta")] public Meta Meta { get; set; }
        [JsonProperty("data")] public ICollection<ProductBalanceDTO> Data { get; set; }

        public GetProductBalancesDTO(ICollection<ProductBalanceDTO> data = null)
        {
            Meta = new Meta();
            Data = data;
        }
    }
}
