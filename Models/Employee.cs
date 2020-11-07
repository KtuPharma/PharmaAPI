using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Employee
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(15)]
        public string PersonalCode { get; set; }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        [Required]
        public DepartmentId Department { get; set; }

        [Required]
        public DateTime RegisterDate { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public DateTime FireDate { get; set; }

        public EmployeeStatusId Status { get; set; }

        public ICollection<Message> Messages { get; set; }

        public ICollection<TruckEmployee> TruckEmployees { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public ICollection<Report> Reports { get; set; }

        public Pharmacy Pharmacy { get; set; }

        public Warehouse Warehouse { get; set; }
    }
}
