using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimetableManager.Domain.Models;

namespace TimetableManager.EntityFramework.Services
{
    class TimeSlotDataService
    {
        private readonly TimetableManagerDbContext _context;

        public TimeSlotDataService(TimetableManagerDbContext context)
        {
            _context = context;
        }
        public async Task<List<TimeSlot>> GetTimeSlotsAsync()
        {
            return await _context.TimeSlots.ToListAsync();
        }
    }
}
