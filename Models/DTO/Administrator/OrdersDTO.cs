using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using API.Models.DTO;
using API.Models.DTO.Administrator;

namespace API.Models.DTO
{
    public class OrdersDTO <T>
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)] public int Id { get; set; }
        [JsonProperty("order_time", NullValueHandling = NullValueHandling.Ignore)] public DateTime OrderTime { get; set; }
        [JsonProperty("delivery_time", NullValueHandling = NullValueHandling.Ignore)] public DateTime DeliveryTime { get; set; }
        [JsonProperty("address_from", NullValueHandling = NullValueHandling.Ignore)] public string AddressFrom { get; set; }
        [JsonProperty("address_to", NullValueHandling = NullValueHandling.Ignore)] public string AddressTo { get; set; }
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)] public OrderStatusId Status { get; set; }
        [JsonProperty("products", NullValueHandling = NullValueHandling.Ignore)] public ICollection<T> Products { get; set; }
        [JsonProperty("employee")] public string Employee { get; set; }
        public OrdersDTO(){}
    }
}
