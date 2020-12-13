using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO.Administrator
{
    public class FilterPharmaciesReportDTO
    {
        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [JsonProperty("datefrom")]
        public DateTime DateFrom { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [JsonProperty("dateto")]
        public DateTime DateTo { get; set; }
    }
}
