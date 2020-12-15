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
    public class ReportsController : ApiControllerBase
    {
        public ReportsController(ApiContext context, UserManager<Employee> userManager) :
        base(context, userManager){ }

        [Authorize(Roles = "Admin")]
        [HttpPost("report")]
        public async Task<ActionResult<IEnumerable<GetDataDTO<PharmacyReportDTO>>>> GetPharmacyReport(FilterPharmacyReportDTO model)
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var pharmacies = await Context.Report
                    .Where(r => r.Pharmacy.Id == model.PharmacyId
                    && DateTime.Compare(r.DateFrom, model.DateFrom) >= 0
                    && DateTime.Compare(r.DateTo, model.DateTo) <= 0)
                    .Select(r => new ReportDTO(
                            r,
                            Context.Employees.FirstOrDefault(e => e.Id == r.Employee.Id)
                            .LastName))
                            .ToListAsync();

            var report = new PharmacyReportDTO(pharmacies);

            return Ok(new GetDataTDTO<PharmacyReportDTO>(report));
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<GetDataDTO<PharmacyReportDTO>>>> GetPharmaciesReport(FilterPharmaciesReportDTO model)
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var pharmacies = await Context.Pharmacy
                                    .Select(z => new PharmacyDTO(z))
                                    .ToListAsync();

            decimal bigger = 0;
            var profit = new List<PharmacyProfitDTO>();
            var report = new PharmaciesReportDTO();

            foreach (var item in pharmacies)
            {
                decimal pharmacyProfit = Context.Report
                   .Where(r => r.Pharmacy.Id == item.Id
                   && DateTime.Compare(r.DateFrom, model.DateFrom) >= 0
                   && DateTime.Compare(r.DateTo, model.DateTo) <= 0).Sum(r => r.OrderAmount);

                profit.Add(new PharmacyProfitDTO(item.Address, pharmacyProfit));
                report.PharmaciesAmount += pharmacyProfit;
                report.PharmaciesCounter++;

                if (bigger <= pharmacyProfit)
                {
                    bigger = pharmacyProfit;
                    report.TopPharmacy = item.Address;
                }
            }
            report.BiggestAmount = bigger;
            report.ProfitByPharmacy = profit;

            return Ok(new GetDataTDTO<PharmaciesReportDTO>(report));
        }
    }
}
