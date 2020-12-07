using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO.Administrator
{
    public class ProductBalanceRegisterDTO
    {

        [Required]
        [JsonProperty("expirationdate")]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [JsonProperty("price")]
        public decimal Price { get; set; }

        [Required]
        [JsonProperty("medicament")]
        public int Medicament { get; set; }

        [JsonProperty("warehouse")]
        public int Warehouse { get; set; }
    }
}
