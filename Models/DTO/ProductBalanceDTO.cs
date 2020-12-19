using Newtonsoft.Json;

namespace API.Models.DTO
{
    public class ProductBalanceDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("inSale")]
        public bool InSale { get; set; }

        public ProductBalanceDTO(ProductBalance pb)
        {
            Id = pb.Id;
            Name = pb.Medicament.Name;
            Price = pb.Price;
            Quantity = pb.Quantity;
            InSale = pb.InSale;
        }
    }
}
