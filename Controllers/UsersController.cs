﻿using API.Models;
using API.Models.DTO;
using API.Models.DTO.Administrator;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces(ApiContentType)]
    public class UsersController : ApiControllerBase
    {
        private readonly JwtService _jwt;

        public UsersController(
            ApiContext context,
            IConfiguration config,
            UserManager<Employee> userManager
        ) :
            base(context, userManager)
        {
            _jwt = new JwtService(config);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user == null)
                return ApiBadRequest("User does not exist.");

            if (await UserManager.CheckPasswordAsync(user, model.Password))
            {
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
        [HttpPost("register")]
        public async Task<IActionResult> Signup(RegisterDTO model)
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            foreach (var validator in UserManager.PasswordValidators)
            {
                var res = await validator.ValidateAsync(UserManager, null, model.Password);
                if (!res.Succeeded)
                    return ApiBadRequest(res.Errors.First().Description);
            }

            var user = new Employee(model);

            switch (model.RoleId)
            {
                case DepartmentId.Pharmacy:
                    user.Pharmacy = Context.Pharmacy.FirstOrDefault(z => z.Id == model.PharmacyWarehouseOrTruck);
                    break;
                case DepartmentId.Warehouse:
                    user.Warehouse = Context.Warehouse.FirstOrDefault(z => z.Id == model.PharmacyWarehouseOrTruck);
                    break;
                case DepartmentId.Transportation:
                    Context.TruckEmployees.Add(new TruckEmployee()
                    {
                        Truck = Context.Truck.FirstOrDefault(z => z.Id == model.PharmacyWarehouseOrTruck),
                        Employee = user
                    });
                    break;
            }

            var result = await UserManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return ApiBadRequest(result.Errors.First().Description);

            string token = _jwt.GenerateSecurityToken(new JwtUser
            {
                Email = user.Email,
                roleId = model.RoleId
            });
            return StatusCode(201);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("status/edit")]
        public async Task<IActionResult> UserStatus(StatusDTO model)
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var user = Context.Employees.FirstOrDefault(z => z.Id == model.Id);
            user.Status = model.Status;
            if (model.Status == EmployeeStatusId.Fired)
            {
                user.Department = DepartmentId.None;
            }
            await UserManager.UpdateAsync(user);
            return Ok();
        }

         [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetDataDTO<UsersDTO>>>> GetUsers()
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var users = await Context.Employees.Select(o => new UsersDTO(o,
                o.Pharmacy != null ? o.Pharmacy.Address : o.Warehouse != null 
                ? o.Warehouse.Address : o.Department == DepartmentId.Transportation 
                ? o.Department.ToString() : DepartmentId.None.ToString()))
                .ToListAsync();

            return Ok(new GetDataDTO<UsersDTO>(users));
        }
    }
}
