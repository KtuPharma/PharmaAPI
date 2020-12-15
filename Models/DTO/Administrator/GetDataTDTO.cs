using Newtonsoft.Json;

namespace API.Models.DTO.Administrator
{
    public class GetDataTDTO<T>
    {
        [JsonProperty("meta")] public Meta Meta { get; set; }
        [JsonProperty("data")] public T Data { get; set; }

        public GetDataTDTO(T orders)
        {
            Meta = new Meta();
            Data = orders;
        }
    }
}
