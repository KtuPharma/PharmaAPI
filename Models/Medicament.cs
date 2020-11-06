using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Medicament
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string ActiveSubstance { get; set; }

        [Required]
        [StringLength(31)]
        public string BarCode { get; set; }

        [Required]
        public bool RecipeRequired { get; set; }

        [Required]
        public bool IsReimbursed { get; set; }

        [Required]
        [StringLength(255)]
        public string Country { get; set; }

        [Required]
        public PharmaceuticalFormId Form { get; set; }

        public ICollection<ProductBalance> Balances { get; set; }
    }
}
