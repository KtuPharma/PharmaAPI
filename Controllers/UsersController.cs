using API.Models;
using API.Models.DTO;
using API.Models.DTO.Administrator;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces(ApiContentType)]
    public class UsersController : ApiControllerBase
    {
        private readonly UserManager<Employee> _userManager;
        private readonly JwtService _jwt;

        public UsersController(
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
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return ApiBadRequest("User does not exist.");

            if (await _userManager.CheckPasswordAsync(user, model.Password))
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

        // [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Signup(RegisterDTO model)
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            foreach (var validator in _userManager.PasswordValidators)
            {
                var res = await validator.ValidateAsync(_userManager, null, model.Password);
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

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return ApiBadRequest(result.Errors.First().Description);

            string token = _jwt.GenerateSecurityToken(new JwtUser
            {
                Email = user.Email,
                roleId = model.RoleId
            });
            return StatusCode(201);
        }

        // [Authorize(Roles = "Admin")]
        [HttpPost("status/edit")]
        public async Task<IActionResult> UserStatus(StatusDTO model)
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var user = Context.Employees.FirstOrDefault(z => z.Id == model.Id);
            user.Status = model.Status;
            await _userManager.UpdateAsync(user);
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
