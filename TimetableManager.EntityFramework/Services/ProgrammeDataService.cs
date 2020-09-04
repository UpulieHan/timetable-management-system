using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<int> DeleteProgramme(int id)
        {
            var p = _context.Programmes.Where(e => e.ProgrammeId == id).First();

            _context.Programmes.Remove(p);

            return await _context.SaveChangesAsync();
        }
        public async Task<Programme> GetProgrammeById(int id)
        {
            return await _context.Programmes.Where(e => e.ProgrammeId == id).FirstAsync();
        }
        public async Task<int> UpdateProgramme(Programme programme, int id)
        {
            Programme p = _context.Programmes.Where(e => e.ProgrammeId == id).First();
            p.ProgrammeFullName = programme.ProgrammeFullName;
            p.ProgrammeShortName = programme.ProgrammeShortName;

            return await _context.SaveChangesAsync();
        }
    }
}
