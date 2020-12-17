using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using API.Models;
using API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MessagesController : ApiControllerBase
    {
        public MessagesController(ApiContext context, UserManager<Employee> userManager) :
            base(context, userManager) { }

        [Authorize(Roles = "Pharmacy,Admin")]
        [HttpGet]
        public async Task<ActionResult<GetDataDTO<GetMessageDTO>>> GetMessages()
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var messages = await Context.Messages.ToListAsync();
            var preparedMessages = new List<GetMessageDTO>();
            foreach (var message in messages)
            {
                await Context.Entry(message).Reference(m => m.Author).LoadAsync();
                string author = $"{message.Author.FirstName} {message.Author.LastName}";
                preparedMessages.Add(new GetMessageDTO(message, author));
            }

            return Ok(new GetDataDTO<GetMessageDTO>(preparedMessages));
        }


        [Authorize(Roles = "Pharmacy,Admin")]
        [HttpPost]
        public async Task<IActionResult> PostMessage(PostMessageDTO messageData)
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var user = await GetCurrentUser();
            var message = new Message(messageData, user);
            await Context.Messages.AddAsync(message);
            await Context.SaveChangesAsync();

            return StatusCode(201);
        }
    }
}
