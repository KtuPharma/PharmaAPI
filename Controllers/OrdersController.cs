using API.Models;
using API.Models.DTO;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using API.Models.DTO.Administrator;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ApiControllerBase
    {
        public OrdersController(ApiContext context) : base(context) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetOrdersDTO<ProductBalance>>>> GetOrders()
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var orders = await Context.Order
              .Select(o => new OrdersDTO<ProductBalance>()
              {
                  Id = o.Id,
                  OrderTime = o.OrderTime,
                  AddressFrom = o.AddressFrom,
                  AddressTo = o.AddressTo,
                  DeliveryTime = o.DeliveryTime,
                  Status = o.Status.ToString(),
                  Price = Context.ProductBalance.Where(y => y.Order.Id == o.Id).Sum(z => z.Price)
              }).ToListAsync();

            return Ok(new GetOrdersDTO<ProductBalance>(orders));
        }
    }
}
