using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

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
    }
}
