using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MedicamentsController : ApiControllerBase
    {
        public MedicamentsController(ApiContext context, UserManager<Employee> userManager) : base(context, userManager) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetMedicamentsDTO>>> GetMedicaments()
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var medicaments = await Context.Medicaments
                .Select(x => new MedicamentDTO(x))
                .ToListAsync();

            return Ok(new GetMedicamentsDTO(medicaments));
        }
    }
}
