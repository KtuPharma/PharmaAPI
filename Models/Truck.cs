using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Truck
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Brand { get; set; }

        [Required]
        [StringLength(255)]
        public string Model { get; set; }

        [Required]
        public float Capacity { get; set; }

        [Required]
        [StringLength(15)]
        public string PlateNumber { get; set; }

        public ICollection<TruckEmployee> TruckEmployees { get; set; }
    }
}
