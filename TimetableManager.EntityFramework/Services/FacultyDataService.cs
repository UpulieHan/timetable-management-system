using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableManager.Domain.Models;

namespace TimetableManager.EntityFramework.Services
{
    public class FacultyDataService
    {
        private readonly TimetableManagerDbContext _context;

        public FacultyDataService(TimetableManagerDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddFaculty(Faculty faculty)
        {
            _context.Faculties.Add(faculty);
            int result = await _context.SaveChangesAsync();

            if(result > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<List<Faculty>> GetFacultiesAsync()
        {
            return await _context.Faculties.Include(e => e.Departments).ToListAsync();
        }
    }
}
