using Newtonsoft.Json;

namespace API.Models.DTO
{
    public class MessageDTO
    {
        [JsonProperty("meta")] public Meta Meta { get; set; }
        [JsonProperty("data")] public MessageData Data { get; set; }

        public MessageDTO(string message)
        {
            Meta = new Meta();
            Data = new MessageData {Message = message};
        }
    }

    public class MessageData
    {
        [JsonProperty("message")] public string Message { get; set; }
    }
}
