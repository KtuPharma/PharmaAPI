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

        public OrderStatus() { }

        public OrderStatus(OrderStatus o)
        {
            Id = o.Id;
            Name = o.Name;
        }
    }
}
