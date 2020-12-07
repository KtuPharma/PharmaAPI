using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO.Administrator
{
    public class TransactionInterDTO
    {
            public int Id { get; set; }

            public decimal Sum { get; set; }

            public DateTime Date { get; set; }

            public TransactionMethodId Method { get; set; }

            [Required]
            public string Employee { get; set; }
    }
}
