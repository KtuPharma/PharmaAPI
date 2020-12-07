using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WarehouseController : ApiControllerBase
    {
        public WarehouseController(ApiContext context) : base(context) { }

        [Authorize(Roles = "Warehouse")]
        [HttpGet]
        public async Task<ActionResult<GetProductBalancesDTO>> GetProductBalances()
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var balances = await Context.ProductBalance
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

            var balance = await Context.ProductBalance
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            balance.InSale = !balance.InSale;
            Context.ProductBalance.Update(balance);
            Context.SaveChanges();

            return Ok(new ProductBalanceDTO(balance));
        }
    }
}
