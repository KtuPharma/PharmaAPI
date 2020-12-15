using Newtonsoft.Json;

namespace API.Models.DTO.Administrator
{
    public class WorplaceDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("pharmacyWarehouseOrTruck")]
        public string Object { get; set; }

        public WorplaceDTO(int id, string o)
        {
            Id = id;
            Object = o;
        }
    }
}
