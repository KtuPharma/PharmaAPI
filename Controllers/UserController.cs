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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtService _jwt;

        public UserController(
            ApiContext context,
            IConfiguration config,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager) :
            base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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

            var result =
                await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, lockoutOnFailure: false);
            if (result.IsLockedOut)
                return ApiBadRequest("User account locked out.");

            if (!result.Succeeded)
                return ApiBadRequest("Invalid username or password.");

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new
            {
                token = _jwt.GenerateSecurityToken(new JwtUser()
                {
                    Username = user.UserName,
                    Email = user.Email,
                    roleId = (DepartmentId)Enum.Parse(typeof(DepartmentId), roles.First(), true)
                })
            });
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Signup(RegisterDTO model)
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            if (!(await _roleManager.RoleExistsAsync(model.RoleId.ToString())))
            {
                await _roleManager.CreateAsync(new IdentityRole(model.RoleId.ToString()));
            }


            var user = new User
            {
                UserName = model.Username ?? model.Email,
                Email = model.Email,
            };

            foreach (var validator in _userManager.PasswordValidators)
            {
                var res = await validator.ValidateAsync(_userManager, null, model.Password);
                if (!res.Succeeded)
                    return ApiBadRequest(res.Errors.First().Description);
            }

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return ApiBadRequest(result.Errors.First().Description);

            var userFromDb = await _userManager.FindByNameAsync(user.UserName);

            var asignedResult = await _userManager.AddToRoleAsync(userFromDb, model.RoleId.ToString());
            if (!asignedResult.Succeeded)
                return ApiBadRequest(asignedResult.Errors.First().Description);

            string token = _jwt.GenerateSecurityToken(new JwtUser
            {
                Username = user.UserName,
                Email = user.Email,
                roleId = model.RoleId
            });

            return Created("", new { token });
        }

        [Authorize(Roles = "Pharmacy")]
        [HttpGet("RoleTest")]
        public async Task<IActionResult> RoleTest1()
        {
            return Ok(HttpContext.User.Identity.Name);
        }
    }
}
