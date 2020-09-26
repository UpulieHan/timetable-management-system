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

        public Task<int> AddSession(Session session, List<Lecturer> lecturers, List<GroupId> groups, Tag tag, Subject subject)
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

           

            return _context.SaveChangesAsync();
        }

        public async Task<List<Session>> GetListAsync()
        {
            return await _context.Sessions
                .Include(l => l.LecturerSessions)
                .ThenInclude(e => e.Lecturer)
                .Include(s => s.Subject)
                .Include(t => t.Tag)
                .ToListAsync();
        }
    }
}
