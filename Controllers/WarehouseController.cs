using System;
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
        public WarehouseController(ApiContext context, UserManager<Employee> userManager) : base(context, userManager) { }

        [Authorize(Roles = "Warehouse")]
        [HttpGet]
        public async Task<ActionResult<GetProductBalancesDTO>> GetProductBalances()
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var balances = await Context.ProductBalances
                .Select(x => new ProductBalanceDTO(x))
                .ToListAsync();

            return Ok(new GetProductBalancesDTO(balances));
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
