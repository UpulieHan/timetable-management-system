using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimetableManager.Domain.Models;


namespace TimetableManager.EntityFramework.Services
{
    public class ProgrammeDataService
    {
        private readonly TimetableManagerDbContext _context;
        public ProgrammeDataService(TimetableManagerDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddProgramme(Programme programme)
        {
            _context.Programmes.Add(programme);
            int result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }

            return false;
        }
        public async Task<List<Programme>> GetProgramme()
        {
            return await _context.Programmes.ToListAsync();
        }

    }
}
