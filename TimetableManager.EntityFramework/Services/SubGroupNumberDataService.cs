﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetableManager.Domain.Models;

namespace TimetableManager.EntityFramework.Services
{
    public class SubGroupNumberDataService
    {
        private readonly TimetableManagerDbContext _context;
        public SubGroupNumberDataService(TimetableManagerDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddSubGroupNumber(SubGroupNumber subGroup)
        {
            _context.SubGroupNumbers.Add(subGroup);
            int result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }

            return false;
        }
        public async Task<List<SubGroupNumber>> GetSubGroupNumbers()
        {
            return await _context.SubGroupNumbers.ToListAsync();
        }
        public async Task<int> DeleteSubGroupNumber(int id)
        {
            var p = _context.SubGroupNumbers.Where(e => e.Id == id).First();

            _context.SubGroupNumbers.Remove(p);

            return await _context.SaveChangesAsync();
        }
        public async Task<SubGroupNumber> GetSubGroupNoById(int id)
        {
            return await _context.SubGroupNumbers.Where(e => e.Id == id).FirstAsync();
        }
        public async Task<int> UpdateSubgroupNo(SubGroupNumber sub, int id)
        {
            SubGroupNumber s = _context.SubGroupNumbers.Where(e => e.Id == id).First();
            s.SubGroupNum = sub.SubGroupNum;
            return await _context.SaveChangesAsync();
        }
    }
}
