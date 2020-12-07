using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using API.Models.DTO.Administrator;

namespace API.Models.DTO
{
    public class ProductBalanceInterDTO
    {
            [Key]
            [Required]
            public int Id { get; set; }

            [Required]
             public DateTime ExpirationDate { get; set; }

            [Required]
            public decimal Price { get; set; }

            [Required]
            public string Medicament { get; set; }

            public TransactionInterDTO Transaction { get; set; }

            public string Pharmacy { get; set; }

            public string Warehouse { get; set; }

            public string Provider { get; set; }
    }
}
