using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.DTO
{
    public class GetProductBalancesDTO
    {
        [JsonProperty("meta")] public Meta Meta { get; set; }
        [JsonProperty("data")] public IEnumerable<ProductBalanceDTO> Data { get; set; }

        public GetProductBalancesDTO(IEnumerable<ProductBalanceDTO> medicaments)
        {
            Meta = new Meta();
            Data = medicaments;
        }
    }
}
