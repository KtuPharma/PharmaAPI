using API.Models;
using System.Collections.Generic;

namespace API.Models.DTO.Administrator
{
    public class MedicineProviderDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public MedicineProviderDTO(MedicineProvider m)
        {
            Id = m.Id;
            Name = m.Name;
            Country = m.Country;
        }
    }
}
