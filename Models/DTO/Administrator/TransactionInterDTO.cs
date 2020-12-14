using System;
using API.Models;

namespace API.Models.DTO.Administrator
{
    public class TransactionInterDTO
    {
            public int Id { get; set; }

            public decimal Sum { get; set; }

            public DateTime Date { get; set; }

            public TransactionMethodId Method { get; set; }

            public string Employee { get; set; }
    }
}
