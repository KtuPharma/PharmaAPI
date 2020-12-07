using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace API.Models.DTO
{
    public class GetMedicineProviderDTO
    {
        [JsonProperty("meta")] public Meta Meta { get; set; }
        [JsonProperty("data")] public IEnumerable<MedicineProviderDTO> Data { get; set; }

        public GetMedicineProviderDTO(IEnumerable<MedicineProviderDTO> providers)
        {
            Meta = new Meta();
            Data = providers;
        }
    }
}
