using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
{
    public class EditOrderDTO
    {
        [Required]
        public int OrderID { get; set; }

        [Required]
        public OrderStatusId Status { get; set; }
    }
}