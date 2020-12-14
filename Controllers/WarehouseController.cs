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

            var user = await GetCurrentUser();
            int id = user.Warehouse.Id;

            var balances = await Context.ProductBalances.Where(g => g.Warehouse.Id == id)
                .Select(o => new ProductBalanceDTO(
                    o,
                    Context.Medicaments.FirstOrDefault(x => x.Id == o.Medicament.Id).Name
                )).ToListAsync();

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
    }
}
