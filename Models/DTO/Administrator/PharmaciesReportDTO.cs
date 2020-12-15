using Newtonsoft.Json;
using System.Collections.Generic;

namespace API.Models.DTO.Administrator
{
    public class PharmaciesReportDTO
    {
        [JsonProperty("pharmaciesAmount")]
        public decimal PharmaciesAmount { get; set; }

        [JsonProperty("topPharmacy ")]
        public string TopPharmacy { get; set; }

        [JsonProperty("biggestAmount")]
        public decimal BiggestAmount { get; set; }

        [JsonProperty("pharmaciesCounter")]
        public int PharmaciesCounter { get; set; }

        [JsonProperty("profitbyPharmacy")]
        public List<PharmacyProfitDTO> ProfitByPharmacy { get; set; }

        public PharmaciesReportDTO() {}
    }
}
