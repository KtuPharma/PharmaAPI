using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public decimal Sum { get; set; }

        public DateTime Date { get; set; }

        public TransactionMethodId Method { get; set; }

        [Required]
        public Employee Pharmacist { get; set; }

        public ICollection<ProductBalance> Products { get; set; }
    }
}
