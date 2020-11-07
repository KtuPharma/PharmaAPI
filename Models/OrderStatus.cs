namespace API.Models
{
    public enum OrderStatusId
    {
        Waiting = 1,
        Preparing,
        Prepared,
        Delivering,
        Delivered,
        Canceled
    }

    public class OrderStatus
    {
        public OrderStatusId Id { get; set; }
        public string Name { get; set; }
    }
}
