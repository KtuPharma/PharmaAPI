using System;
using System.ComponentModel.DataAnnotations;
using API.Models.DTO.Administrator;

namespace API.Models
{
    public class Report
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public decimal OrderAmount { get; set; }

        [Required]
        public DateTime DateFrom { get; set; }

        [Required]
        public DateTime DateTo { get; set; }

        [Required]
        public Employee Employee { get; set; }

        [Required]
        public Pharmacy Pharmacy { get; set; }

        public Report() { }

        public Report(decimal sum, FilterPharmaciesReportDTO dates, Employee employee, Pharmacy pharmacy)
        {
            OrderAmount = sum;
            DateFrom = dates.DateFrom;
            DateTo = dates.DateTo;
            Employee = employee;
            Pharmacy = pharmacy;
        }
    }
}
