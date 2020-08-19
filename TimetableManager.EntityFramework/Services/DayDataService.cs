using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimetableManager.Domain.Models;


namespace TimetableManager.EntityFramework.Services
{
    public class DayDataService
    {
        private readonly TimetableManagerDbContext _context;

        public DayDataService(TimetableManagerDbContext context)
        {
            _context = context;
        }
        public async Task<List<Day>> GetDaysAsync()
        {
            return await _context.Days.ToListAsync();
        }
    }
}
