﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetableManager.Domain.Models;

namespace TimetableManager.EntityFramework.Services
{
    public class GroupIdDataService
    {
        private readonly TimetableManagerDbContext _context;
        public GroupIdDataService(TimetableManagerDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddGroupId(GroupId groupId)
        {
            _context.GroupIds.Add(groupId);
            int result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }

            return false;
        }
        public async Task<List<GroupId>> GetGroupId()
        {
            return await _context.GroupIds.ToListAsync();
        }
        public async Task<int> DeleteGroupId(int id)
        {
            var p = _context.GroupIds.Where(e => e.Id == id).First();

            _context.GroupIds.Remove(p);

            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteAllGroupId()
        {
            foreach (var id in _context.GroupIds.Select(e => e.Id))
            {
                var entity = new GroupId { Id = id };
                _context.GroupIds.Attach(entity);
                _context.GroupIds.Remove(entity);
            }

            return await _context.SaveChangesAsync();
        }

    }
}
