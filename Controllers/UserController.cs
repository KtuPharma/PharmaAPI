using API.Models;
using API.Models.DTO;
using API.Models.DTO.Administrator;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using API.Models.DTO.Administrator;
using System.Collections;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    [Produces(ApiContentType)]
    public class UserController : ApiControllerBase
    {
        private readonly UserManager<Employee> _userManager;
        private readonly JwtService _jwt;

        public UserController(
            ApiContext context,
            IConfiguration config,
            UserManager<Employee> userManager
            ) :
            base(context)
        {
            _jwt = new JwtService(config);
            _userManager = userManager;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return ApiBadRequest("User does not exist.");

            if (await _userManager.CheckPasswordAsync(user, model.Password)) {
                return Ok(new
                {
                    token = _jwt.GenerateSecurityToken(new JwtUser()
                    {
                        Email = user.Email,
                        roleId = user.Department
                    })
                });
            }
            
            return ApiBadRequest("Bad password");
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Signup(RegisterDTO model)
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var user = new Employee
            {
                PersonalCode = model.PersonalCode,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,
                Department = model.RoleId,
                BirthDate = model.BirthDate,
                Status = EmployeeStatusId.Employed,
                RegisterDate = DateTime.Now
            };

            if (model.WorkPlace)
            {
                user.Pharmacy = Context.Pharmacy.Where(z => z.Id == model.PharmacyOrWarehouse).FirstOrDefault();
            }
            else
            {
                user.Warehouse = Context.Warehouse.Where(z => z.Id == model.PharmacyOrWarehouse).FirstOrDefault();
            }
            foreach (var validator in _userManager.PasswordValidators)
            {
                var res = await validator.ValidateAsync(_userManager, null, model.Password);
                if (!res.Succeeded)
                    return ApiBadRequest(res.Errors.First().Description);
            }

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return ApiBadRequest(result.Errors.First().Description);
                        
            string token = _jwt.GenerateSecurityToken(new JwtUser
            {
                Email = user.Email,
                roleId = model.RoleId
            });

            return Created("", new { token });
        }

        [HttpPost("userstatus")]
        public async Task<IActionResult> UserStatus(StatusDTO model) {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }
            Employee user = Context.Employees.Where(z => z.Id == model.Id).FirstOrDefault();
            user.Status = model.Status;
            Context.Employees.Update(user);
            await Context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("providersignup")]
        public async Task<IActionResult> ProviderSignup(MedicineProviderRegisterDTO model)
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
            Context.MedicineProvider.Add(provider);
            await Context.SaveChangesAsync();
            IList<ProductBalanceRegisterDTO> iList = model.Products as IList<ProductBalanceRegisterDTO>;
            //ProductBalance product = null;
            var prod = Context.MedicineProvider
                .Select(z => new MedicineProvider() { 
                        Id = z.Id,
                        Name = z.Name,
                        Country = z.Country,
                        Status = z.Status
                }).ToList().Last();
            Medicament medicament = Context.Medicaments.Where(z => z.Id == iList[0].Medicament).FirstOrDefault();
             for (int i = 0; i < iList.Count; i++)
             {
                ProductBalance product = new ProductBalance { 
                   ExpirationDate = iList[i].ExpirationDate,
                   Price = iList[i].Price,
                   Medicament = Context.Medicaments.Where(z => z.Id == iList[i].Medicament).FirstOrDefault(),
                   Warehouse = Context.Warehouse.Where(z => z.Id == iList[i].Warehouse).FirstOrDefault(),
                   Provider = prod
                };
                Context.ProductBalance.Add(product);
                await Context.SaveChangesAsync();
            }
            int t = 0;
            for (int i = 0; i < 10; i++)
            {
                t++;
            }
            //atsifiltruojam last id ir pagal tai sudedam warehouses
            return Ok();
        }
            [Authorize]
        [HttpGet()]
        public IActionResult GetAllUsers()
        {
            return Ok(Context.Employees.ToList());
        }

        [Authorize(Roles = "Pharmacy")]
        [HttpGet("RoleTest")]
        public IActionResult RoleTest1()
        {
            return Ok(HttpContext.User.Identity.Name);
        }
    }
}
//Truck Employee Problema nežinoma kur jis dirba