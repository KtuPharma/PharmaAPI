using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.Models.DTO.Administrator;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
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
            base(context, userManager) { }

        [Authorize(Roles = "Admin")]
        [HttpPost("report")]
        public async Task<ActionResult<GetDataTDTO<PharmacyReportDTO>>> GetPharmacyReport(
            FilterPharmacyReportDTO model)
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

        [Authorize(Roles = "Pharmacy")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateReport(FilterPharmaciesReportDTO model)
        {
            var user = await GetCurrentUser();
            decimal sum = Context.Order
                .Where(o => o.Employee == user)
                .Where(o => DateTime.Compare(o.OrderTime, model.DateFrom) >= 0)
                .Where(o => DateTime.Compare(o.OrderTime, model.DateTo) <= 0)
                .Include(o => o.Products)
                .SelectMany(o => o.Products)
                .Sum(pb => pb.Price * pb.Quantity);

            var report = new Report(sum, model, user, user.Pharmacy);
            await Context.Report.AddAsync(report);
            await Context.SaveChangesAsync();

            return StatusCode(201);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<GetDataTDTO<PharmacyReportDTO>>> GetPharmaciesReport(
            FilterPharmaciesReportDTO model)
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
