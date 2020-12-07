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
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ApiControllerBase
    {

        private readonly UserManager<Employee> _userManager;

        public OrdersController(ApiContext context, UserManager<Employee> userManager) :
            base(context) {
            _userManager = userManager; 
        }

        [HttpGet("orders")] //return a list of orders without products
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
                  DeliveryTime = o.DeliveryTime,
                  AddressFrom = o.AddressFrom,
                  AddressTo = o.AddressTo,
                  Status = o.Status,
                  Employee = Context.Employees.Where(y => y.PersonalCode == o.Employee.PersonalCode).First().FirstName
              }).ToListAsync();
            return Ok(new GetOrdersDTO<ProductBalance>(orders));
        }

        [HttpGet("ordersf")] //return a full list of orders and products
        public async Task<ActionResult<IEnumerable<GetOrdersDTO<ProductBalanceInterDTO>>>> GetOrders2()
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var orders = await Context.Order
                .Select(o => new OrdersDTO<ProductBalanceInterDTO>(){
                    Id = o.Id,
                    OrderTime = o.OrderTime,
                    DeliveryTime = o.DeliveryTime,
                    AddressFrom = o.AddressFrom,
                    AddressTo = o.AddressTo,
                    Status = o.Status,
                    Products = Context.ProductBalance
                                .Where(x => x.Order.Id == o.Id)
                                .Select(x => new ProductBalanceInterDTO() { 
                                        Id = x.Id,
                                        ExpirationDate = x.ExpirationDate,
                                        Price = x.Price,
                                        Medicament = Context.Medicaments.Where(y => y.Id == x.Id).First().Name,
                                        Transaction = Context.Transaction.Where(y => y.Id == x.Id)
                                                            .Select(y => new TransactionInterDTO() { 
                                                                Id = y.Id,
                                                                Sum = y.Sum,
                                                                Date = y.Date,
                                                                Method = y.Method,
                                                                Employee = Context.Employees.Where(z => z.PersonalCode == y.Pharmacist.PersonalCode).First().FirstName
                                                            }).First(),
                                        Pharmacy = Context.Pharmacy.Where(y => y.Id == x.Id).First().Address,
                                        Warehouse = Context.Warehouse.Where(y => y.Id == x.Id).First().Address,
                                        Provider = Context.MedicineProvider.Where(y => y.Id == x.Id).First().Name

                                }).ToList()
                }).ToListAsync();           

            return Ok(new GetOrdersDTO<ProductBalanceInterDTO>(orders));
        }

        [HttpPost("registerprovider")]
        public async Task<IActionResult> RegisterProvider(MedicineProvider model)
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            if (!Context.MedicineProvider.Select(x => x.Name).Contains(model.Name))
            {

            }

            var message = new MessageDTO("Hello World!");
            return Ok(message);
        }
    }
}
