using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableManager.Domain.Models;

namespace TimetableManager.EntityFramework.Services
{
    public class SubjectDataService
    {
        private readonly TimetableManagerDbContext _context;

        public SubjectDataService(TimetableManagerDbContext context)
        {
            _context = context;
        }

        public Task<int> AddSubject(Subject subject)
        {
            _context.Subjects.Add(subject);
            return _context.SaveChangesAsync();
        }

        public async Task<List<Subject>> GetSubjectsAsync()
        {
            return await _context.Subjects
                    .Include(e => e.SubjectPreferredRooms)
                    .ThenInclude(e => e.Room)
                    .ToListAsync();
        }

        public async Task<int> DeleteSubject(int id)
        {
            var subject = _context.Subjects.Where(e => e.Id == id).First();
            _context.Subjects.Remove(subject);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateSubject(Subject s)
        {
            Subject subject = _context.Subjects.Where(e => e.SubjectCode == s.SubjectCode).First();
            subject.SubjectName = s.SubjectName;
            subject.LectureHours = s.LectureHours;
            subject.TutorialHours = s.TutorialHours;
            subject.LabHours = s.LabHours;
            subject.EvaluationHours = s.EvaluationHours;
            subject.OfferedYearSemester = s.OfferedYearSemester;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> SetPrefferedRoom(Subject subject, Room room)
        {
            var s = _context.Subjects.Single(e => e.Id == subject.Id);
            var r = _context.Rooms.Single(e => e.RoomId == room.RoomId);

            _context.Set<SubjectPreferredRoom>().Add(new SubjectPreferredRoom { Subject = s, Room = r });

            return await _context.SaveChangesAsync();
        }
    }
}
