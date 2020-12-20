using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models.DTO.Administrator;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WarehouseController : ApiControllerBase
    {
        public WarehouseController(ApiContext context, UserManager<Employee> userManager) :
            base(context, userManager) { }

        [Authorize(Roles = "Pharmacy")]
        [HttpGet]
        public async Task<ActionResult<GetDataDTO<GetWarehouseDTO>>> GetWarehouses()
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var warehouses = await Context.Warehouse
                .Select(w => new GetWarehouseDTO(w))
                .ToListAsync();

            return Ok(new GetDataDTO<GetWarehouseDTO>(warehouses));
        }

        [Authorize(Roles = "Pharmacy, Warehouse")]
        [HttpGet("{id}/products")]
        public async Task<ActionResult<GetDataDTO<ProductBalanceDTO>>> GetProductBalances(int id)
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var products = await Context.ProductBalances
                .Where(pb => pb.Warehouse.Id == id)
                .ToListAsync();

            var productList = new List<ProductBalanceDTO>();
            foreach (var product in products)
            {
                await Context.Entry(product).Reference(pb => pb.Medicament).LoadAsync();
                productList.Add(new ProductBalanceDTO(product));
            }

            return Ok(new GetDataDTO<ProductBalanceDTO>(productList));
        }

        [Authorize(Roles = "Warehouse")]
        [HttpPost("{id}")]
        public async Task<ActionResult<ProductBalanceDTO>> UpdateProductBalance(int id)
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var balance = await Context.ProductBalances
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            balance.InSale = !balance.InSale;
            Context.ProductBalances.Update(balance);
            Context.SaveChanges();

            return Ok(new ProductBalanceDTO(balance));
        }

        [Authorize(Roles = "Pharmacy")]
        [HttpPost("{id}/order")]
        public async Task<IActionResult> PlaceOrder(int id, IList<OrderRequestDTO> requestedProducts)
        {
            if (!IsValidApiRequest()) return ApiBadRequest("Invalid Headers!");
            if (!AreProductsValid(id, requestedProducts)) return ApiBadRequest("Invalid product requested!");

            var user = await GetCurrentUser();
            var warehouse = await Context.Warehouse
                .Where(w => w.Id == id)
                .Include(w => w.Products)
                .FirstAsync();

            var order = new Order(warehouse, user.Pharmacy.Address, user);
            var orderProducts = new List<ProductBalance>();
            foreach (var requestedProduct in requestedProducts)
            {
                var product = GetProductInfoFromDto(requestedProduct);
                var warehouseBalance = await Context.ProductBalances
                    .Where(pb => pb.Id == product.Id)
                    .Include(pb => pb.Medicament)
                    .Include(pb => pb.Provider)
                    .FirstAsync();
                var newBalance = new ProductBalance(
                    warehouseBalance,
                    requestedProduct.Quantity,
                    warehouse, user.Pharmacy
                );
                orderProducts.Add(newBalance);
            }

            order.Products = orderProducts;
            await Context.ProductBalances.AddRangeAsync(orderProducts);
            await Context.Order.AddAsync(order);
            await Context.SaveChangesAsync();

            return StatusCode(201);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Addresses")]
        public async Task<ActionResult<IEnumerable<GetDataDTO<WorplaceDTO>>>> GetWarehousesAddresses()
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var pharmacies = await Context.Warehouse
                .Select(z => new WorplaceDTO(z.Id, z.Address))
                .ToListAsync();
            return Ok(new GetDataDTO<WorplaceDTO>(pharmacies));
        }

        private bool AreProductsValid(int id, IEnumerable<OrderRequestDTO> data)
        {
            return data.Select(requestProduct => Context.ProductBalances
                    .Where(pb => pb.Id == requestProduct.ProductBalanceId)
                    .Include(pb => pb.Medicament)
                    .Select(pb => pb.Warehouse.Id)
                    .First())
                .All(warehouseId => warehouseId == id);
        }

        private ProductInfo GetProductInfoFromDto(OrderRequestDTO requestProduct)
        {
            return Context.ProductBalances
                .Include(pb => pb.Medicament)
                .Where(pb => pb.Id == requestProduct.ProductBalanceId)
                .Select(pb => new ProductInfo(pb))
                .First();
        }
    }
}
