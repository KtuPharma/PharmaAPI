using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using API.Models.DTO.Administrator;

namespace API.Models.DTO
{
    public class ProductBalanceInterDTO
    {

            public DateTime ExpirationDate { get; set; }

            public decimal Price { get; set; }

            public string Medicament { get; set; }

            public TransactionInterDTO Transaction { get; set; }

            public string Provider { get; set; }
    }
}
