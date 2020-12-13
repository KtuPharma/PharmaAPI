using System.Collections.Generic;
using Newtonsoft.Json;

namespace API.Models.DTO
{
    public class GetDataDTO<T>
    {
        [JsonProperty("meta")] public Meta Meta { get; set; }
        [JsonProperty("data")] public IEnumerable<T> Data { get; set; }

        public GetDataDTO(IEnumerable<T> orders)
        {
            Meta = new Meta();
            Data = orders;
        }
    }
}
