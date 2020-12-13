using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class MedicineProvider
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Country { get; set; }

        [Required]
        public bool Status { get; set; }

        public ICollection<ProductBalance> Products { get; set; }

        public ICollection<ProviderWarehouse> ProviderWarehouses { get; set; }

        public MedicineProvider(){}
    }
}
