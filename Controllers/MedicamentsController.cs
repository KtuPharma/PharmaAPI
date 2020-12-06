using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MedicamentsController : ApiControllerBase
    {
        public MedicamentsController(ApiContext context) : base(context) { }

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

        [HttpPost("{id}")]
        public async Task<ActionResult<MedicamentDTO>> UpdateMedicament(int id)
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
            Context.SaveChanges();

            return Ok(new MedicamentDTO(medicament));
        }
    }
}
