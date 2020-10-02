using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimetableManager.Domain.Models;

namespace TimetableManager.EntityFramework.Services
{
    public class CenterDataService
    {
        private readonly TimetableManagerDbContext _context;

        public CenterDataService(TimetableManagerDbContext context)
        {
            _context = context;
        }
        public async Task<List<Center>> GetCentersAsync()
        {
            return await _context.Centers.Include(e => e.Buildings).ToListAsync();
        }

    }
}
