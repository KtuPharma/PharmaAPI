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
        public async Task<ActionResult<GetProductBalancesDTO>> GetWarehouses()
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
        public async Task<ActionResult<GetProductBalancesDTO>> GetProductBalances(int id)
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
    }
}
