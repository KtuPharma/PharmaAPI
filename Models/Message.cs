using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Message
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Topic { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public Employee Author { get; set; }
    }
}
