using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimetableManager.Domain.Models;

namespace TimetableManager.EntityFramework.Services
{
    public class TimeSlotDataService
    {
        private readonly TimetableManagerDbContext _context;

        public TimeSlotDataService(TimetableManagerDbContext context)
        {
            _context = context;
        }
        public async Task<List<TimeSlot>> GetTimeSlotsAsync()
        {
            return await _context.TimeSlots
                    .Include(e => e.LecturerUnavailableTimeSlots)
                    .ThenInclude(e => e.Lecturer)
                    .ToListAsync();
        }
    }
}
