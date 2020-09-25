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

        public Task<int> AddSession(Session session, List<Lecturer> lecturers, List<GroupId> groups)
        {
            _context.Sessions.Add(session);
            return _context.SaveChangesAsync();
        }
    }
}
