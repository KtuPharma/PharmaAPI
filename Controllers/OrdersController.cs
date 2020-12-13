using API.Models;
using API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ApiControllerBase
    {
        public OrdersController(ApiContext context, UserManager<Employee> userManager) : base(context, userManager) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetDataDTO<OrdersDTO>>>> GetOrders()
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var orders = await Context.Order
                .Select(o => new OrdersDTO(
                    o,
                    Context.ProductBalances
                        .Where(y => y.Order.Id == o.Id)
                        .Sum(z => z.Price)
                )).ToListAsync();

            return Ok(new GetDataDTO<OrdersDTO>(orders));
        }


        [Authorize(Roles = "Warehouse")]
        [HttpGet("byID")]
        public async Task<ActionResult<GetDataDTO<OrdersDTO>>> GetOrdersByWarehouse()
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            string email = User.FindFirst(ClaimTypes.Email).Value;
            var user = await UserManager.FindByEmailAsync(email);
            int id = user.Warehouse.Id;

            var orders = await Context.Order.Where(g => g.Warehouse.Id == id)
               .Select(o => new OrdersDTO(
                    o,
                    Context.ProductBalances
                        .Where(y => y.Order.Id == o.Id)
                        .Sum(z => z.Price)
                )).ToListAsync();
            return Ok(new GetDataDTO<OrdersDTO>(orders));
        }

        [Authorize(Roles = "Warehouse")]
        [HttpPost("changeStatus")]
        public ActionResult<EditOrderDTO> ChangeOrderTransportationStatus(EditOrderDTO model)
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var order = Context.Order.Where(x => x.Id == model.OrderID).First();

            order.Status = model.Status;
            Context.Order.Update(order);
            Context.SaveChanges();
            return Ok();
        }
    }
}
