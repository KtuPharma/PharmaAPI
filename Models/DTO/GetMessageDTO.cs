using System;
using Newtonsoft.Json;

namespace API.Models.DTO
{
    public class GetMessageDTO
    {
        [JsonProperty("topic")]
        public string Topic { get; }

        [JsonProperty("text")]
        public string Text { get; }

        [JsonProperty("date")]
        public DateTime Date { get; }

        [JsonProperty("author")]
        public string Author { get; }

        public GetMessageDTO(Message m, string author)
        {
            Topic = m.Topic;
            Text = m.Text;
            Date = m.Date;
            Author = author;
        }
    }
}
