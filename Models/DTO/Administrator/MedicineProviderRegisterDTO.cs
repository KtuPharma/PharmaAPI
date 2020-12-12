using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace API.Models.DTO.Administrator
{
    public class MedicineProviderRegisterDTO
    {

        [Required]
        [StringLength(255)]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("products", NullValueHandling = NullValueHandling.Ignore)]
        public IList<ProductBalanceRegisterDTO> Products { get; set; }

        //a list of wherehouses that have access to order diferrent products
        [Required]
        [JsonProperty("warehouse")]
        public IList<int> Warehouse { get; set; } 
    }
}
