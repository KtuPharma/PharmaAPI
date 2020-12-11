using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace API.Models.DTO
{
    public class OrdersDTO
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; }

        [JsonProperty("order_time", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime OrderTime { get; set; }

        [JsonProperty("delivery_time", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime DeliveryTime { get; set; }

        [JsonProperty("address_from", NullValueHandling = NullValueHandling.Ignore)]
        public string AddressFrom { get; set; }

        [JsonProperty("address_to", NullValueHandling = NullValueHandling.Ignore)]
        public string AddressTo { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        [JsonProperty("products", NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<ProductBalance> Products { get; set; }

        [JsonProperty("employee", NullValueHandling = NullValueHandling.Ignore)]
        public string Employee { get; set; }

        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Price { get; set; }

        public OrdersDTO(Order order, decimal price)
        {
            Id = order.Id;
            OrderTime = order.OrderTime;
            AddressFrom = order.AddressFrom;
            AddressTo = order.AddressTo;
            DeliveryTime = order.DeliveryTime;
            Status = order.Status.ToString();
            Price = price;
        }
    }
}
