﻿using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.Models.DTO.Administrator;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using API.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StatusController : ApiControllerBase
    {
        public StatusController(ApiContext context, UserManager<Employee> userManager) :
        base(context, userManager){ }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetDataDTO<EmployeeStatus>>>> GetProviderStatus()
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var status = await Context.EmployeeStatus
                .Select(e => new EmployeeStatus(e))
                .ToListAsync();
            return Ok(new GetDataDTO<EmployeeStatus>(status));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("role")]
        public async Task<ActionResult<IEnumerable<GetDataDTO<Department>>>> GetRole()
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var role = await Context.Department
                .Select(r => new Department(r))
                .ToListAsync();
            return Ok(new GetDataDTO<Department>(role));
        }
    }
}
