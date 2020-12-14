using Newtonsoft.Json;
using System.Collections.Generic;

namespace API.Models.DTO.Administrator
{
    public class PharmaciesReportDTO
    {
        [JsonProperty("pharmaciesamount")]
        public decimal PharmaciesAmount { get; set; }

        [JsonProperty("toppharmacy ")]
        public string TopPharmacy { get; set; }

        [JsonProperty("biggestamount")]
        public decimal BiggestAmount { get; set; }

        [JsonProperty("pharmaciescounter")]
        public int PharmaciesCounter { get; set; }

        [JsonProperty("profitbypharmacy")]
        public List<PharmacyProfitDTO> ProfitByPharmacy { get; set; }

        public PharmaciesReportDTO() {}
    }
}
