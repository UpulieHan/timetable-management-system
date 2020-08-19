using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimetableManager.Domain.Models;

namespace TimetableManager.EntityFramework.Services
{
    public class GroupNumberDataService
    {
        private readonly TimetableManagerDbContext _context;
        public GroupNumberDataService(TimetableManagerDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddGroupNumber(GroupNumber groupNumber)
        {
            _context.GroupNumbers.Add(groupNumber);
            int result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }

            return false;
        }
        public async Task<List<GroupNumber>> GetGroupNumbers()
        {
            return await _context.GroupNumbers.ToListAsync();
        }
    }
}
