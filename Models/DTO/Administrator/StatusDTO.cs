using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace API.Models.DTO.Administrator
{
    public class StatusDTO
    {
        [Required]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required]
        [JsonProperty("status")]
        public EmployeeStatusId Status { get; set; }
    }
}
