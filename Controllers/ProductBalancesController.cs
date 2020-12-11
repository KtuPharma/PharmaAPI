using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductBalancesController : ApiControllerBase
    {
        public ProductBalancesController(ApiContext context, UserManager<Employee> userManager) :
            base(context, userManager) { }

        [Authorize(Roles = "Pharmacy,Warehouse")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetMedicamentsDTO>>> GetProductBalances()
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var user = await GetCurrentUser();
            List<ProductBalance> balances;
            switch (user.Department)
            {
                case DepartmentId.Pharmacy:
                    balances = await Context.ProductBalances
                        .Where(pb => pb.Pharmacy.Id == user.Pharmacy.Id)
                        .ToListAsync();
                    break;
                case DepartmentId.Warehouse:
                    balances = await Context.ProductBalances
                        .Where(pb => pb.Warehouse.Id == user.Warehouse.Id)
                        .ToListAsync();
                    break;
                default:
                    return NotAllowedError("This action is not allowed!");
            }

            var dtoList = PrepareBalanceData(balances);
            return Ok(new GetProductBalancesDTO(dtoList));
        }

        private ICollection<ProductBalanceDTO> PrepareBalanceData(IEnumerable<ProductBalance> products)
        {
            var productList = new List<ProductBalanceDTO>();
            foreach (var product in products)
            {
                Context.Entry(product).Reference(pb => pb.Medicament).Load();
                productList.Add(new ProductBalanceDTO(product));
            }

            return productList;
        }
    }
}
