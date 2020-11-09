using API.Models;
using API.Models.DTO;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly JwtService _jwt;

        public UserController(
            ApiContext context,
            IConfiguration config,
            UserManager<User> userManager,
            SignInManager<User> signInManager) :
            base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwt = new JwtService(config);
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(AuthDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return ApiBadRequest("User does not exist.");

            var result =
                await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, lockoutOnFailure: false);
            if (result.IsLockedOut)
                return ApiBadRequest("User account locked out.");

            if (!result.Succeeded)
                return ApiBadRequest("Invalid username or password.");

            return Ok(new
            {
                token = _jwt.GenerateSecurityToken(new JwtUser()
                {
                    Username = user.UserName,
                    Email = user.Email
                })
            });
        }


        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Signup(AuthDTO model)
        {
            var user = new User
            {
                UserName = model.Username ?? model.Email,
                Email = model.Email
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

            string token = _jwt.GenerateSecurityToken(new JwtUser
            {
                Username = user.UserName,
                Email = user.Email
            });

            return Created("", new {token});
        }

        [HttpGet("Me")]
         public async Task<IActionResult> getme()
        {
            return Ok( HttpContext.User.Identity.Name);
        }

    }
}
