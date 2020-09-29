using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
            return await _context.Subjects.ToListAsync();
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
    }
}
