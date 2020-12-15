using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO.Administrator
{
    public class FilterPharmacyReportDTO
    {
        [Required]
        [JsonProperty("pharmacyId")]
        public int PharmacyId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [JsonProperty("dateFrom")]
        public DateTime DateFrom { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [JsonProperty("dateTo")]
        public DateTime DateTo { get; set; }
    }
}
