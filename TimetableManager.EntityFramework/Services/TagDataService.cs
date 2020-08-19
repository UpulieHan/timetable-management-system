using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimetableManager.Domain.Models;

namespace TimetableManager.EntityFramework.Services
{
    public class TagDataService
    {
        private readonly TimetableManagerDbContext _context;
        public TagDataService(TimetableManagerDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddTag(Tag tag)
        {
            _context.Tags.Add(tag);
            int result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }

            return false;
        }
        public async Task<List<Tag>> GetTags()
        {
            return await _context.Tags.ToListAsync();
        }
    }
}
