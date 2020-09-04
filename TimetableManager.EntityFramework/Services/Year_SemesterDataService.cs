using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<int> DeleteYear_Semester(int id)
        {
            var p = _context.Year_Semesters.Where(e => e.YsId == id).First();

            _context.Year_Semesters.Remove(p);

            return await _context.SaveChangesAsync();
        }
        public async Task<Year_Semester> GetYsById(int id)
        {
            return await _context.Year_Semesters.Where(e => e.YsId == id).FirstAsync();
        }
        public async Task<int> UpdateYs(Year_Semester ys,int id)
        {
            Year_Semester newys = await _context.Year_Semesters.Where(e => e.YsId == id).FirstAsync();
            newys.YsYear = ys.YsYear;
            newys.YsSemester = ys.YsSemester;
            newys.YsShortName = ys.YsShortName;

            return await _context.SaveChangesAsync();
        }
    }
}
