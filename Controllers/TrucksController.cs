using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.Models.DTO.Administrator;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Collections;
using API.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TrucksController : ApiControllerBase
    {
        public TrucksController(ApiContext context, UserManager<Employee> userManager) :
        base(context, userManager){ }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetDataDTO<WorplaceDTO>>>> GetTrucks()
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var trucks = await Context.Truck
                .Select(z => new WorplaceDTO(z.Id, z.PlateNumber))
                .ToListAsync();
            return Ok(new GetDataDTO<WorplaceDTO>(trucks));
        }
    }
}
