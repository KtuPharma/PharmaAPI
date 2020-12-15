using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace API.Models.DTO.Administrator
{
    public class PharmacyReportDTO
    {
        [JsonProperty("allAmount")]
        public decimal AllAmount { get; set; }

        public IEnumerable<ReportDTO> Pharmacy { get; set; }

        public PharmacyReportDTO(IEnumerable<ReportDTO> r)
        {
            AllAmount = r.Sum(s => s.OrderAmount);
            Pharmacy = r;
        }
    }
}
