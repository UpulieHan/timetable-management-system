using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableManager.Domain.Models;

namespace TimetableManager.EntityFramework.Services
{
    public class SubGroupIdDataService
    {
        private readonly TimetableManagerDbContext _context;
        public SubGroupIdDataService(TimetableManagerDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddSubGroupId(SubGroupId subGroupId)
        {
            _context.SubGroupIds.Add(subGroupId);
            int result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }

            return false;
        }
        public async Task<List<SubGroupId>> GetSubGroupId()
        {
            return await _context.SubGroupIds.ToListAsync();
        }
        public async Task<int> DeleteSubGroupId(int id)
        {
            var p = _context.SubGroupIds.Where(e => e.Id == id).First();

            _context.SubGroupIds.Remove(p);

            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteAllSubGroupId()
        {
            foreach (var id in _context.SubGroupIds.Select(e => e.Id))
            {
                var entity = new SubGroupId { Id = id };
                _context.SubGroupIds.Attach(entity);
                _context.SubGroupIds.Remove(entity);
            }

            return await _context.SaveChangesAsync();
        }
    }
}
