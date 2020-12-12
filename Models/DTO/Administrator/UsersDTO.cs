using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System;

namespace API.Models.DTO.Administrator
{
    public class UsersDTO
    {
        [Required]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required]
        [JsonProperty("personal_code")]
        public string PersonalCode { get; set; }

        [Required]
        [JsonProperty("username")]
        public string Username { get; set; }

        [Required]
        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        [Required]
        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [Required]
        [JsonProperty("email")]
        public string Email { get; set; }

        [Required]
        [JsonProperty("department")]
        public string Department { get; set; }

        [Required]
        [JsonProperty("register_date")]
        public DateTime RegisterDate { get; set; }

        [Required]
        [JsonProperty("birthdate")]
        public DateTime BirthDate { get; set; }

        [Required]
        [JsonProperty("status")]
        public string Status { get; set; }

        [Required]
        [JsonProperty("workplace")]
        public string WorkPlace { get; set; }

        public UsersDTO(Employee e, string workPlace)
        {
            Id = e.Id;
            PersonalCode = e.PersonalCode;
            Username = e.Username;
            FirstName = e.FirstName;
            LastName = e.LastName;
            Email = e.Email;
            Department = e.Department.ToString();
            RegisterDate = e.RegisterDate;
            BirthDate = e.BirthDate;
            Status = e.Status.ToString();
            WorkPlace = workPlace;
        }
    }
}
