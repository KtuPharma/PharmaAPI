using Newtonsoft.Json;

namespace API.Models.DTO
{
    public class PostMessageDTO
    {
        [JsonProperty("topic")]
        public string Topic { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
