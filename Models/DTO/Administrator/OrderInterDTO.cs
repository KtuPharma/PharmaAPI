using System.Collections.Generic;
using Newtonsoft.Json;

namespace API.Models.DTO.Administrator
{
    public class OrderInterDTO
    {
        public OrdersDTO Order { get; set; }

        [JsonProperty("product_balances")]
        public IEnumerable<ProductBalanceInterDTO> ProductBalances { get; set; }

        public OrderInterDTO(OrdersDTO order, IEnumerable<ProductBalanceInterDTO> productBalances)
        {
            Order = order;
            ProductBalances = productBalances;
        }
    }
}
