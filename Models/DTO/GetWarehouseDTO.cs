using Newtonsoft.Json;

namespace API.Models.DTO
{
    public class GetWarehouseDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        public GetWarehouseDTO(Warehouse w)
        {
            Id = w.Id;
            Address = $"{w.Address}, {w.City}, {w.Country}";
        }
    }
}
