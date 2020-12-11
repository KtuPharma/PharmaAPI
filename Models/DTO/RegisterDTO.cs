using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace API.Models.DTO
{
    public class RegisterDTO
    {

        [Required]
        [JsonProperty("personalCode")]
        public string PersonalCode { get; set; }

        [Required]
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [Required]
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [Required]
        [JsonProperty("username")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [JsonProperty("email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Neįvestas slapyvardis!")]
        [JsonProperty("password")]
        [MinLength(8, ErrorMessage = "Minimalus slaptažodžio ilgis 8 simboliai!")]
        public string Password { get; set; }

        [Required]
        [JsonProperty("roleId")]
        public DepartmentId RoleId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [JsonProperty("birthDate")]
        public DateTime BirthDate { get; set; }

        [Required]
        [JsonProperty("pharmacywarehouseortruck")]
        public int PharmacyWarehouseOrTruck { get; set; }
    }
}
