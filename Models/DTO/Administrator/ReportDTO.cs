using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.DTO;

namespace API.Models.DTO.Administrator
{
    public class ReportDTO
    {

        public decimal OrderAmount { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public string Employee { get; set; }

        public ReportDTO(Report r, string employee)
        {
            OrderAmount = r.OrderAmount;
            DateFrom = r.DateFrom;
            DateTo = r.DateTo;
            Employee = employee;
        }
    }
}
