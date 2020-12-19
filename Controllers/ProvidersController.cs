using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.Models.DTO.Administrator;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using API.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProvidersController : ApiControllerBase
    {
        public ProvidersController(ApiContext context, UserManager<Employee> userManager) :
            base(context, userManager) { }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin, Warehouse")]
        [HttpGet]
        public async Task<ActionResult<GetDataDTO<MedicineProviderDTO>>> GetProviders()
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var user = await GetCurrentUser();
            IList<MedicineProvider> providers = null;
            switch (user.Department)
            {
                case DepartmentId.Admin:
                    providers = await Context.MedicineProvider
                        .Where(m => m.Status)
                        .ToListAsync();
                    break;
                case DepartmentId.Warehouse:
                    await Context.Entry(user.Warehouse)
                        .Collection(w => w.ProviderWarehouses)
                        .LoadAsync();

                    foreach (var pw in user.Warehouse.ProviderWarehouses)
                    {
                        await Context.Entry(pw)
                            .Reference(w => w.Provider)
                            .LoadAsync();
                    }

                    providers = user.Warehouse.ProviderWarehouses
                        .Select(pw => pw.Provider)
                        .Where(p => p.Status)
                        .ToList();
                    break;
                default:
                    return NotAllowedError("This action is not allowed!");
            }

            var preparedProviders = providers.Select(p => new MedicineProviderDTO(p));
            return Ok(new GetDataDTO<MedicineProviderDTO>(preparedProviders));
        }

        [Authorize(Roles = "Warehouse")]
        [HttpGet("{id}/products")]
        public async Task<ActionResult<GetDataDTO<ProductBalanceDTO>>> GetProducts(int id)
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var products = await Context.ProductBalances
                .Where(pb => pb.Provider.Id == id)
                .Where(pb => pb.Warehouse == null)
                .Where(pb => pb.Order == null)
                .Where(pb => pb.Pharmacy == null)
                .Include(pb => pb.Medicament)
                .Select(pb => new ProductBalanceDTO(pb))
                .ToListAsync();

            return Ok(new GetDataDTO<ProductBalanceDTO>(products));
        }

        [Authorize(Roles = "Warehouse")]
        [HttpPost("{id}/order")]
        public async Task<ActionResult<GetDataDTO<OrderFromProviderDTO>>> OrderToWarehouse(int id,
            IList<OrderFromProviderDTO> model)
        {
            if (!IsValidApiRequest()) return ApiBadRequest("Invalid Headers!");
            if (!AreProductsValid(id, model)) return ApiBadRequest("Invalid product requested!");

            var user = await GetCurrentUser();
            await Context.Entry(user.Warehouse).Collection(w => w.Products).LoadAsync();
            foreach (var requestProduct in model)
            {
                var product = GetProductInfoFromDto(requestProduct);

                if (AppendBalance(user.Warehouse.Products, product, requestProduct.Quantity)) continue;

                var providerBalance = await Context.ProductBalances
                    .Where(pb => pb.Id == product.Id)
                    .Include(pb => pb.Medicament)
                    .Include(pb => pb.Provider)
                    .FirstAsync();
                var newBalance = new ProductBalance(providerBalance, requestProduct.Quantity, user.Warehouse);
                await Context.ProductBalances.AddAsync(newBalance);
            }

            await Context.SaveChangesAsync();
            return Ok();
        }

        private bool AreProductsValid(int id, IEnumerable<OrderFromProviderDTO> data)
        {
            return data.Select(requestProduct => Context.ProductBalances
                    .Where(pb => pb.Id == requestProduct.ProductBalanceId)
                    .Include(pb => pb.Medicament)
                    .Select(pb => pb.Provider.Id)
                    .First())
                .All(providerId => providerId == id);
        }

        private bool AppendBalance(IEnumerable<ProductBalance> products, ProductInfo product,
            int quantity)
        {
            foreach (var balance in products)
            {
                Context.Entry(balance).Reference(pb => pb.Medicament).Load();
                if (balance.Medicament.Id == product.MedicamentId)
                {
                    balance.Quantity += quantity;
                    Context.ProductBalances.Update(balance);
                    return true;
                }
            }

            return false;
        }

        private ProductInfo GetProductInfoFromDto(OrderFromProviderDTO requestProduct)
        {
            return Context.ProductBalances
                .Include(pb => pb.Medicament)
                .Where(pb => pb.Id == requestProduct.ProductBalanceId)
                .Select(pb => new ProductInfo(pb))
                .First();
        }
    }
}
