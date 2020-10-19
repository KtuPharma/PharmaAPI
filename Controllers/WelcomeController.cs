using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class WelcomeController : ApiControllerBase
    {
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
