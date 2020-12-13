using System.Collections.Generic;
using Newtonsoft.Json;

namespace API.Models.DTO.Administrator
{
    public class PharmacyReportDTO
    {
        [JsonProperty("allamount")]
        public decimal AllAmount { get; set; }

        public IEnumerable<ReportDTO> Pharmacy { get; set; }

        public PharmacyReportDTO(IEnumerable<ReportDTO> r)
        {
            AllAmount = 0;
            Pharmacy = r;
        }
    }
}
