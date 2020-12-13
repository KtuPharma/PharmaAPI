using Newtonsoft.Json;
using System.Collections.Generic;

namespace API.Models.DTO.Administrator
{
    public class PharmaciesReportDTO
    {
        [JsonProperty("allamount")]
        public decimal AllAmount { get; set; }

        [JsonProperty("mostprofitablepharmacy ")]
        public string MostProfitablePharmacy { get; set; }

        [JsonProperty("numberofpharmacies ")]
        public int NumberOfPharmacies { get; set; }

        [JsonProperty("pharmacyprofit ")]
        public List<PharmacyProfitDTO> PharmacyProfit { get; set; }

        public PharmaciesReportDTO() {}
    }
}
