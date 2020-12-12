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
    public class ProvidersController : ApiControllerBase
    {
        public ProvidersController(ApiContext context, UserManager<Employee> userManager) :
            base(context, userManager) { }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddProvider(MedicineProviderRegisterDTO model)
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var provider = new MedicineProvider
            {
                Name = model.Name,
                Country = model.Country,
                Status = true
            };

            foreach (var products in model.Products)
            {
                Context.ProductBalances.Add(new ProductBalance()
                {
                    ExpirationDate = products.ExpirationDate,
                    Price = products.Price,
                    Medicament = Context.Medicaments.FirstOrDefault(z => z.Id == products.Medicament),
                    Provider = provider
                });
            }

            foreach (var warehouse in model.Warehouse)
            {
                Context.ProviderWarehouse.Add(new ProviderWarehouse()
                {
                    WarehouseId = warehouse,
                    Provider = provider
                });
            }

            await Context.SaveChangesAsync();
            return Ok();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("{id}")]
        public async Task<IActionResult> ProviderStatus(int id)
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var provider = Context.MedicineProvider.FirstOrDefault(z => z.Id == id);
            provider.Status = !provider.Status;
            Context.MedicineProvider.Update(provider);
            await Context.SaveChangesAsync();
            return Ok();
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetDataDTO<MedicineProviderDTO>>>> GetProviders()
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var provider = await Context.MedicineProvider
                .Where(m => m.Status)
                .Select(z => new MedicineProviderDTO(z))
                .ToListAsync();
            return Ok(new GetDataDTO<MedicineProviderDTO>(provider));
        }
    }
}
