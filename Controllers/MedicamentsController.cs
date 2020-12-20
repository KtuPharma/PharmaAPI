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
    public class MedicamentsController : ApiControllerBase
    {
        public MedicamentsController(ApiContext context, UserManager<Employee> userManager) :
            base(context, userManager) { }

        [Authorize(Roles = "Warehouse, Pharmacy")]
        [HttpGet]
        public async Task<ActionResult<GetDataDTO<MedicamentDTO>>> GetMedicaments()
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var medicaments = await Context.Medicaments
                .Select(x => new MedicamentDTO(x))
                .ToListAsync();

            return Ok(new GetDataDTO<MedicamentDTO>(medicaments));
        }

        [Authorize(Roles = "Warehouse")]
        [HttpPost("{id}")]
        public async Task<ActionResult> UpdateMedicament(int id)
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var medicament = await Context.Medicaments
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            medicament.InSale = !medicament.InSale;
            Context.Medicaments.Update(medicament);
            await Context.SaveChangesAsync();

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Names")]
        public async Task<ActionResult<IEnumerable<GetDataDTO<MedicamentsNameDTO>>>> GetMedicamentsNames()
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var medicaments = await Context.Medicaments
                .Select(z => new MedicamentsNameDTO(z.Id, z.Name))
                .ToListAsync();

            return Ok(new GetDataDTO<MedicamentsNameDTO>(medicaments));
        }
    }
}
