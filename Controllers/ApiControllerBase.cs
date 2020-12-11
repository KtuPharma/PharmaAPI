using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using API.Models.DTO;

namespace API.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        private const string ApiHeader = "X-Api-Request";
        protected const string ApiContentType = "application/json";

        protected ApiContext Context { get; }

        public ApiControllerBase(ApiContext context)
        {
            Context = context;
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
    }
}
