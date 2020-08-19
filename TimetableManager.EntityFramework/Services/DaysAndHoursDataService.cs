using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableManager.Domain.Models;

namespace TimetableManager.EntityFramework.Services
{
    public class DaysAndHoursDataService
    {
        private readonly TimetableManagerDbContext _context;

        public DaysAndHoursDataService(TimetableManagerDbContext context)
        {
            _context = context;
        }
        public async Task<List<DaysAndHours>> GetDaysAndHoursAsync()
        {
            return await _context.DaysAndHours.ToListAsync();
        }
    }
}
