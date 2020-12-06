using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.DTO
{
    public class ProductBalanceDTO
    {
        [JsonProperty("name")] public string Name { get; }

        public ProductBalanceDTO(ProductBalance m)
        {
            Name = m.Medicament.Name;
      
        }
    }
}
