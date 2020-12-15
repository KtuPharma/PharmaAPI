using Newtonsoft.Json;

namespace API.Models.DTO.Administrator
{
    public class MedicamentsNameDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("medicament")]
        public string Name { get; set; }

        public MedicamentsNameDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
