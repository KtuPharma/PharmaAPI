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

        [Authorize(Roles = "Admin")]
        [HttpPost("adduser")]
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

            switch (model.RoleId)
            {
                case DepartmentId.Pharmacy:
                    user.Pharmacy = Context.Pharmacy.Where(z => z.Id == model.PharmacyWarehouseOrTruck).FirstOrDefault();
                    break;
                case DepartmentId.Warehouse:
                    user.Warehouse = Context.Warehouse.Where(z => z.Id == model.PharmacyWarehouseOrTruck).FirstOrDefault();
                    break;
                case DepartmentId.Transportation:
                    Context.TruckEmployees.Add(new TruckEmployee() {
                        Truck = Context.Truck.Where(z => z.Id == model.PharmacyWarehouseOrTruck).FirstOrDefault(),
                        Employee = user
                    });
                    break;
            }

            if (model.RoleId != DepartmentId.Transportation)
            {
                Context.Employees.Add(user);
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

        [Authorize(Roles = "Admin")]
        [HttpPost("addprovider")]
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

            for (int i = 0; i < model.Products.Count; i++)
             {
                Context.ProductBalance.Add(new ProductBalance() 
                { 
                   ExpirationDate = model.Products[i].ExpirationDate,
                   Price = model.Products[i].Price,
                   Medicament = Context.Medicaments.Where(z => z.Id == model.Products[i].Medicament).FirstOrDefault(),
                   Provider = provider
                });
            }

            for (int i = 0; i < model.Warehouse.Count; i++)
            {
                Context.ProviderWarehouse.Add(new ProviderWarehouse()
                {
                    WarehouseId = model.Warehouse[i],
                    Provider = provider
                });
            }
            await Context.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("providerstatus/{id}")]
        public async Task<IActionResult> ProviderStatus(int id)
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }
            MedicineProvider provider = Context.MedicineProvider.Where(z => z.Id == id).FirstOrDefault();
            provider.Status = !provider.Status;
            Context.MedicineProvider.Update(provider);
            await Context.SaveChangesAsync();
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