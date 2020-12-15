using API.Models;
using API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using API.Models.DTO.Administrator;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ApiControllerBase
    {
        public OrdersController(ApiContext context, UserManager<Employee> userManager) : base(context, userManager) { }

        [Authorize(Roles = "Admin")]
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
        [HttpGet("{id}")]
        public async Task<ActionResult<GetDataDTO<OrdersDTO>>> GetOrdersByWarehouse(int id)
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

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

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}/products")]
        public async Task<ActionResult<GetDataDTO<ProductBalanceInterDTO>>> GetOrderProductsByOrder(int id)
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var products = await Context.ProductBalances.Where(p => p.Order.Id == id)
                .Select(p => new ProductBalanceInterDTO() {
                        ExpirationDate = p.ExpirationDate,
                        Price = p.Price,
                        Medicament = Context.Medicaments.FirstOrDefault(m => m.Id == p.Medicament.Id).Name,
                        Provider = Context.MedicineProvider.FirstOrDefault(m => m.Id == p.Provider.Id).Name
            }).ToListAsync();

            return Ok(new GetDataDTO<ProductBalanceInterDTO>(products));
        }

        [Authorize(Roles = "Pharmacy, Warehouse, Transportation, Admin")]
        [HttpGet("{id}/order")]
        public async Task<ActionResult<GetDataTDTO<OrderInterDTO>>> GetFullOrderById(int id)
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var order =  (Context.Order.Select(o => new OrdersDTO(
                                                o, Context.ProductBalances
                                                .Where(y => y.Order.Id == id)
                                                .Sum(z => z.Price))).ToList())
                                                .First(r => r.Id == id);


            var products = await Context.ProductBalances.Where(p => p.Order.Id == id)
                .Select(p => new ProductBalanceInterDTO()
                {
                    ExpirationDate = p.ExpirationDate,
                    Price = p.Price,
                    Medicament = Context.Medicaments.FirstOrDefault(m => m.Id == p.Medicament.Id).Name,
                    Provider = Context.MedicineProvider.FirstOrDefault(m => m.Id == p.Provider.Id).Name
                }).ToListAsync();

            var orderData = new OrderInterDTO(order, products);

            return Ok(new GetDataTDTO<OrderInterDTO>(orderData));
        }

    }
}
