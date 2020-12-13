using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.DTO.Administrator
{
    public class PharmacyProfitDTO
    {
        public string Address { get; set; }

        public decimal Amount { get; set; }

        public PharmacyProfitDTO() {}

        public PharmacyProfitDTO(string address, decimal amount)
        {
            Address = address;
            Amount = amount;
        }
    }
}
