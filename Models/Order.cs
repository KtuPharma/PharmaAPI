﻿using API.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Order
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime OrderTime { get; set; }

        [Required]
        public DateTime DeliveryTime { get; set; }

        [Required]
        [StringLength(255)]
        public string AddressFrom { get; set; }

        [Required]
        [StringLength(255)]
        public string AddressTo { get; set; }

        [Required]
        public OrderStatusId Status { get; set; }

        public Warehouse Warehouse { get; set; }

        public ICollection<ProductBalance> Products { get; set; }
        
        [Required]
        public Employee Employee { get; set; }
    }
}
