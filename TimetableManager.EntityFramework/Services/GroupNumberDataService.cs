using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<int> DeleteGroupNumbers(int id)
        {
            var grpNo = _context.GroupNumbers.Where(e => e.Id == id).First();

            _context.GroupNumbers.Remove(grpNo);

            return await _context.SaveChangesAsync();
        }
        public async Task<GroupNumber> GetGroupNoById(int id)
        {
            return await _context.GroupNumbers.Where(e => e.Id == id).FirstAsync();
        }
        public async Task<int> UpdateGroupNo(GroupNumber groupNumber, int id)
        {
            await this.DeleteGroupNumbers(id);
            _context.GroupNumbers.Add(groupNumber);
            return await _context.SaveChangesAsync();
        }
    }
}
