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

        [Authorize(Roles = "Admin, Warehouse, Transportation, Pharmacy")]
        [HttpGet]
        public async Task<ActionResult<GetDataDTO<OrdersDTO>>> GetOrders()
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var user = await GetCurrentUser();
            IList<Order> orders;
            switch (user.Department)
            {
                case DepartmentId.Admin:
                    orders = await Context.Order.ToListAsync();
                    break;
                case DepartmentId.Warehouse:
                    orders = await Context.Order.Where(o =>
                            o.Warehouse.Id == user.Warehouse.Id &&
                            o.Status != OrderStatusId.Delivered)
                        .ToListAsync();
                    break;
                case DepartmentId.Transportation:
                    orders = await Context.Order.Where(o =>
                        o.Status == OrderStatusId.Prepared ||
                        o.Status == OrderStatusId.Delivering).ToListAsync();
                    break;
                case DepartmentId.Pharmacy:
                    orders = await Context.Order.Where(o =>
                        o.Employee == user).ToListAsync();
                    break;
                default:
                    return NotAllowedError("This action is not allowed!");
            }

            var preparedOrders = orders.Select(o =>
                new OrdersDTO(
                    o,
                    Context.ProductBalances
                        .Where(pb => pb.Order.Id == o.Id)
                        .Sum(s => s.Price)
                ));

            return Ok(new GetDataDTO<OrdersDTO>(preparedOrders));
        }

        [Authorize(Roles = "Warehouse, Transportation")]
        [HttpPost("changeStatus")]
        public async Task<ActionResult<IEnumerable<GetDataDTO<EditOrderDTO>>>> ChangeOrderTransportationStatus(
            EditOrderDTO model)
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var user = await GetCurrentUser();

            switch (user.Department)
            {
                case DepartmentId.Warehouse:
                    if (model.Status == OrderStatusId.Delivering || model.Status == OrderStatusId.Delivered)
                        return NotAllowedError("This action is not allowed!");
                    break;
                case DepartmentId.Transportation:
                    if (model.Status < OrderStatusId.Delivering)
                        return NotAllowedError("This action is not allowed!");
                    break;
                default:
                    return NotAllowedError("This action is not allowed!");
            }

            var order = Context.Order.First(x => x.Id == model.OrderID);

            order.Status = model.Status;
            Context.Order.Update(order);
            await Context.SaveChangesAsync();
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
                .Select(p => new ProductBalanceInterDTO()
                {
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

            var order = (Context.Order.Select(o => new OrdersDTO(
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

        [Authorize(Roles = "Transportation")]
        [HttpGet("{id}/{days}")]
        public async Task<ActionResult<GetDataDTO<ProductBalanceInterDTO>>> PushDeliveryDateLater(int id, double days)
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            if (days < 1 || days > 7)
            {
                return ApiBadRequest("Days count should be between 1 and 7");
            }

            var order = Context.Order.FirstOrDefault(o => o.Id == id);
            order.DeliveryTime = order.DeliveryTime.AddDays(days);
            Context.Order.Update(order);
            Context.SaveChanges();
            return Ok();
        }
    }
}
