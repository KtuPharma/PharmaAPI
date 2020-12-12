using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.Models.DTO.Administrator;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PharmaciesController : ApiControllerBase
    {

        public PharmaciesController(ApiContext context, UserManager<Employee> userManager) :
        base(context, userManager){ }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetDataDTO<PharmacyDTO>>>> GetProviders()
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var pharmacies = await Context.Pharmacy
                .Select(z => new PharmacyDTO(z))
                .ToListAsync();
            return Ok(new GetDataDTO<PharmacyDTO>(pharmacies));
        }
    }
}
