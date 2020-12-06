using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace API.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductBalancesController : ApiControllerBase
    {
        public ProductBalancesController(ApiContext context, UserManager<Employee> userManager) :
            base(context, userManager) { }

        [Authorize(Roles = "Pharmacy")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetProductBalances()
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var user = await GetCurrentUser();
            switch (user.Department)
            {
                case DepartmentId.Pharmacy:
                    // TODO: Get products by PharmacyId
                    break;
                case DepartmentId.Warehouse:
                    // TODO: Get products by WarehouseId
                    break;
                default:
                    return NotAllowedError("This action is not allowed!");
            }

            return Ok(user);
        }
    }
}
