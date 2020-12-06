using System.Linq;
using API.Models.Seed;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class Seeder
    {
        private readonly ApiContext _context;

        public Seeder(ApiContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            _context.Database.EnsureCreated();

            var testObj = _context.Medicaments?.FirstOrDefault(e => e.Id == 1);
            if (testObj != null) return;

            MedicamentSeed.EnsureCreated(_context);

            _context.SaveChanges();
        }
    }
}
