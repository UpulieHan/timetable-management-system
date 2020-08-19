using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimetableManager.Domain.Models;


namespace TimetableManager.EntityFramework.Services
{
    public class Year_SemesterDataService
    {
        private readonly TimetableManagerDbContext _context;
        public Year_SemesterDataService(TimetableManagerDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddYs(Year_Semester ys)
        {
            _context.Year_Semesters.Add(ys);
            int result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }

            return false;
        }
        public async Task<List<Year_Semester>> GetYs()
        {
            return await _context.Year_Semesters.ToListAsync();
        }
    }
}
