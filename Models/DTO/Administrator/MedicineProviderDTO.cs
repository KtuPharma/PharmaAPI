using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace API.Models.DTO
{
    public class MedicineProviderDTO
    {
        [JsonProperty("id")] public int Id { get; }
        [JsonProperty("name")] public string Name { get; }
        [JsonProperty("country")] public string Country { get; }
        [JsonProperty("products")] public ICollection<ProductBalance> Products { get; }
        [JsonProperty("provider_warehouse")] public ICollection<ProviderWarehouse> ProviderWarehouses { get; }


        public MedicineProviderDTO(MedicineProvider m)
        {
            Id = m.Id;
            Name = m.Name;
            Country = m.Country;
            Products = m.Products;
            ProviderWarehouses = m.ProviderWarehouses;
        }
    }
}
