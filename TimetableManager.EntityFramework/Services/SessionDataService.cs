using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableManager.Domain.Models;

namespace TimetableManager.EntityFramework.Services
{
    public class SessionDataService
    {
        private readonly TimetableManagerDbContext _context;

        public SessionDataService(TimetableManagerDbContext context)
        {
            _context = context;
        }

        public Task<int> AddSession(Session session, List<Lecturer> lecturers, List<GroupId> groups, List<SubGroupId> subGroups, Tag tag, Subject subject)
        {
            var t = _context.Tags.Include(e => e.Sessions).Single(e => e.TagId == tag.TagId);
            t.Sessions.Add(session);
            var sub = _context.Subjects.Include(e => e.Sessions).Single(e => e.Id == subject.Id);
            sub.Sessions.Add(session);

            var newSession = _context.Sessions.Add(session);
            lecturers.ForEach(l =>
            {
                var lecturer = _context.Lecturers.Single(e => e.Id == l.Id);
                _context.Set<LecturerSession>().Add(new LecturerSession { Lecturer = lecturer, Session = session });
            });

            if(groups.Count != 0)
            {
                groups.ForEach(e =>
                {
                    var g = _context.GroupIds.Single(i => i.Id == e.Id);
                    _context.Set<GroupIdSession>().Add(new GroupIdSession { Group = g, Session = session });
                });
            }
            else if(subGroups.Count != 0)
            {
                subGroups.ForEach(e =>
                {
                    var g = _context.SubGroupIds.Single(i => i.Id == e.Id);
                    _context.Set<SubGroupIdSession>().Add(new SubGroupIdSession { SubGroup = g, Session = session });
                });
            }
           

            return _context.SaveChangesAsync();
        }

        public async Task<List<Session>> GetListAsync()
        {
            return await _context.Sessions
                .Include(l => l.LecturerSessions)
                .ThenInclude(e => e.Lecturer)
                .Include(s => s.Subject)
                .Include(t => t.Tag)
                .Include(g => g.GroupIdSessions)
                .ThenInclude(gs => gs.Group)
                .Include(sg => sg.SubGroupIdSessions)
                .ThenInclude(sgs => sgs.SubGroup)
                .ToListAsync();
        }
    }
}
