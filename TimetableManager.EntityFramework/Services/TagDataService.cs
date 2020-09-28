using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return await _context.Tags
                    .Include(e => e.TagPreferredRooms)
                    .ThenInclude(e => e.Room)
                    .ToListAsync();
        }
        public async Task<int> DeleteTag(int id)
        {
            var p = _context.Tags.Where(e => e.TagId == id).First();

            _context.Tags.Remove(p);

            return await _context.SaveChangesAsync();
        }
        public async Task<Tag> GetTagById(int id)
        {
            return await _context.Tags.Where(e => e.TagId == id).FirstAsync();
        }
        public async Task<int> UpdateTag(Tag tag, int id)
        {
            Tag t = await _context.Tags.Where(e => e.TagId == id).FirstAsync();
            t.TagName = tag.TagName;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> SetPrefferedRoom(Tag tag, Room room)
        {
            var t = _context.Tags.Single(e => e.TagId == tag.TagId);
            var r = _context.Rooms.Single(e => e.RoomId == room.RoomId);

            _context.Set<TagPreferredRoom>().Add(new TagPreferredRoom { Tag = t, Room = r });

            return await _context.SaveChangesAsync();
        }
    }
}
