using Newtonsoft.Json;

namespace API.Models.DTO
{
    public class ObjectDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        public ObjectDTO(int id, string o)
        {
            Id = id;
            Object = o;
        }
    }
}
