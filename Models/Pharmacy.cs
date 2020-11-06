using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Pharmacy
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
        public decimal Cash { get; set; }

        [Required]
        public decimal Revenue { get; set; }

        public ICollection<Report> Reports { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public ICollection<Register> Registers { get; set; }

        public ICollection<ProductBalance> Products { get; set; }

        public ICollection<PharmacyWarehouse> PharmacyWarehouses { get; set; }
    }
}
