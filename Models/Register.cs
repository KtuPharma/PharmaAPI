using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Register
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Model { get; set; }

        [Required]
        [StringLength(255)]
        public decimal Cash { get; set; }

        [Required]
        public Pharmacy Pharmacy { get; set; }
    }
}
