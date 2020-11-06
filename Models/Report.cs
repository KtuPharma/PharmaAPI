using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Report
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int OrderAmount { get; set; }

        [Required]
        public DateTime DateFrom { get; set; }

        [Required]
        public DateTime DateTo { get; set; }

        [Required]
        public Employee Employee { get; set; }

        [Required]
        public Pharmacy Pharmacy { get; set; }
    }
}
