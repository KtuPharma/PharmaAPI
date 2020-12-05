using API.Models;
using API.Models.DTO;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    [Produces(ApiContentType)]
    public class UserController : ApiControllerBase
    {
        private readonly UserManager<Employee> _userManager;
        //private readonly SignInManager<Employee> _signInManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtService _jwt;

        public UserController(
            ApiContext context,
            IConfiguration config,
            UserManager<Employee> userManager
            //SignInManager<Employee> signInManager,
            //RoleManager<IdentityRole> roleManager
            ) :
            base(context)
        {
            _userManager = userManager;
            //_signInManager = signInManager;
            //_roleManager = roleManager;
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

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return ApiBadRequest("User does not exist.");

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password)) {
                return Ok(new
                {
                    token = _jwt.GenerateSecurityToken(new JwtUser()
                    {
                        Username = user.Username,
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
                Username = model.Username,
                Email = model.Email,
                Department = model.RoleId,
                FirstName = "Test",
                LastName = "Testing",
                PersonalCode = "39000000000"
            };

            foreach (var validator in _userManager.PasswordValidators)
            {
                var res = await validator.ValidateAsync(_userManager, null, model.Password);
                if (!res.Succeeded)
                    return ApiBadRequest(res.Errors.First().Description);
            }

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return ApiBadRequest("AAA" + result.Errors.First().Description);
            
            var userFromDb = await _userManager.FindByNameAsync(model.Username);
            
            string token = _jwt.GenerateSecurityToken(new JwtUser
            {
                Username = user.Username,
                Email = user.Email,
                roleId = model.RoleId
            });

            return Created("", new { token });
        }

        [Authorize]
        [HttpGet()]
        public async Task<IActionResult> GetAllUsers() {
            return Ok(Context.Employees.ToList());
        }

        [Authorize(Roles = "Pharmacy")]
        [HttpGet("RoleTest")]
        public async Task<IActionResult> RoleTest1()
        {
            return Ok(HttpContext.User.Identity.Name);
        }
    }
}
