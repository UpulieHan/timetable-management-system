using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimetableManager.Domain.Models;

namespace TimetableManager.EntityFramework.Services
{
    public class LevelDataService
    {
        private readonly TimetableManagerDbContext _context;

        public LevelDataService(TimetableManagerDbContext context)
        {
            _context = context;
        }

        public async Task<List<Level>> GetLevelsAsync()
        {
            return await _context.Levels.ToListAsync();
        }
    }
}
