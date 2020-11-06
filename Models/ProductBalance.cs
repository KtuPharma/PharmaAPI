﻿using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class ProductBalance
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public Medicament Medicament { get; set; }

        public Order Order { get; set; }

        public Transaction Transaction { get; set; }

        public Pharmacy Pharmacy { get; set; }

        public Warehouse Warehouse { get; set; }

        public MedicineProvider Provider { get; set; }
    }
}
