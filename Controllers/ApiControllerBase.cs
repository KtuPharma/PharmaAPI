using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace API.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        private const string ApiHeader = "X-Api-Request";
        protected const string ApiContentType = "application/json";

        protected ApiContext Context { get; }
        protected readonly UserManager<Employee> UserManager;

        public ApiControllerBase(ApiContext context, UserManager<Employee> userManager)
        {
            Context = context;
            UserManager = userManager;
        }

        protected async Task<Employee> GetCurrentUser()
        {
            string email = User.FindFirst(ClaimTypes.Email).Value;
            var user = await UserManager.FindByEmailAsync(email);
            return user;
        }

        protected bool IsValidApiRequest()
        {
            Request.Headers.TryGetValue(ApiHeader, out var headers);
            if (headers.Count != 1 || string.IsNullOrWhiteSpace(headers.FirstOrDefault()))
            {
                return false;
            }

            return headers == "true";
        }

        protected BadRequestObjectResult ApiBadRequest(string message)
        {
            var error = new ErrorDTO
            {
                Type = 400,
                Title = message
            };
            return BadRequest(error);
        }

        protected NotFoundObjectResult ApiNotFound(string message)
        {
            var error = new ErrorDTO
            {
                Type = 404,
                Title = message
            };
            return NotFound(error);
        }

        protected ObjectResult InternalServerError(string message, string details = null)
        {
            var error = new ErrorDTO
            {
                Type = 500,
                Title = message,
                Details = details
            };
            return StatusCode(500, error);
        }

        protected ObjectResult NotImplementedError(string message, string details = null)
        {
            var error = new ErrorDTO
            {
                Type = 501,
                Title = message,
                Details = details
            };
            return StatusCode(501, error);
        }

        protected ObjectResult NotAllowedError(string message, string details = null)
        {
            var error = new ErrorDTO
            {
                Type = 405,
                Title = message,
                Details = details
            };
            return StatusCode(405, error);
        }
    }
}
