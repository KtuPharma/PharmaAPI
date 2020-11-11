using API.Models;
using API.Models.DTO;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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
        public async Task<IActionResult> Login(AuthDTO model)
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
                    roleId = (DepartmentId)Int32.Parse(roles.First())
                })
            });
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Signup(AuthDTO model)
        {
            if (!(await _roleManager.RoleExistsAsync("1")))
            {
                await _roleManager.CreateAsync(new IdentityRole("1"));
            }
            if (!(await _roleManager.RoleExistsAsync("2")))
            {
                await _roleManager.CreateAsync(new IdentityRole("2"));
            }
            if (!(await _roleManager.RoleExistsAsync("3")))
            {
                await _roleManager.CreateAsync(new IdentityRole("3"));
            }
            if (!(await _roleManager.RoleExistsAsync("4")))
            {
                await _roleManager.CreateAsync(new IdentityRole("4"));
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

            if (model.RoleId.ToString() == "1")
                await _userManager.AddToRoleAsync(userFromDb, "1");
            if (model.RoleId.ToString() == "2")
                await _userManager.AddToRoleAsync(userFromDb, "2");
            if (model.RoleId.ToString() == "3")
                await _userManager.AddToRoleAsync(userFromDb, "3");
            if (model.RoleId.ToString() == "4")
                await _userManager.AddToRoleAsync(userFromDb, "4");

            string token = _jwt.GenerateSecurityToken(new JwtUser
            {
                Username = user.UserName,
                Email = user.Email,
                roleId = model.RoleId
            });

            return Created("", new {token});
        }

        [Authorize(Roles = "Pharmacy")]
        [HttpGet("RoleTest")]
        public async Task<IActionResult> RoleTest1()
        {
            return Ok( HttpContext.User.Identity.Name);
        }
    }
}
