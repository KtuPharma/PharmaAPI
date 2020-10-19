using Newtonsoft.Json;

namespace API.Models
{
    public class MessageDTO
    {
        [JsonProperty("meta")] public Meta Meta { get; set; }
        [JsonProperty("data")] public Data Data { get; set; }

        public MessageDTO(string message)
        {
            Meta = new Meta();
            Data = new Data {Message = message};
        }
    }

    public partial class Data
    {
        [JsonProperty("message")] public string Message { get; set; }
    }
}
