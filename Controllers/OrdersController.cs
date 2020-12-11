using API.Models;
using API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ApiControllerBase
    {
        public OrdersController(ApiContext context) : base(context) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetOrdersDTO>>> GetOrders()
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var orders = await Context.Order
                .Select(o => new OrdersDTO(
                    o,
                    Context.ProductBalance
                        .Where(y => y.Order.Id == o.Id)
                        .Sum(z => z.Price)
                )).ToListAsync();

            return Ok(new GetOrdersDTO(orders));
        }
    }
}
