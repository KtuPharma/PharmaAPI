using API.Models;
using API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class WelcomeController : ApiControllerBase
    {
        public WelcomeController(ApiContext context) : base(context) { }

        [HttpGet]
        public ActionResult<string> GetQuizMessage()
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var message = new MessageDTO("Hello World!");
            return Ok(message);
        }
    }
}
