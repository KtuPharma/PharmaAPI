using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Warehouse
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        [StringLength(255)]
        public string City { get; set; }

        [Required]
        [StringLength(255)]
        public string Country { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public ICollection<ProductBalance> Products { get; set; }

        public ICollection<PharmacyWarehouse> PharmacyWarehouses { get; set; }

        public ICollection<ProviderWarehouse> ProviderWarehouses { get; set; }
    }
}
