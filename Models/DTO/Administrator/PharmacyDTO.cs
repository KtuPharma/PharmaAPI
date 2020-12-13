using API.Models;

namespace API.Models.DTO.Administrator
{
    public class PharmacyDTO
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public PharmacyDTO(Pharmacy p)
        {
            Id = p.Id;
            Address = p.Address;
            City = p.City;
        }
    }
}
