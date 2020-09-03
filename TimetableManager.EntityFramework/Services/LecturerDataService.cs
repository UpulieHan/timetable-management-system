using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableManager.Domain.Models;

namespace TimetableManager.EntityFramework.Services
{
    public class LecturerDataService
    {
        private readonly TimetableManagerDbContext _context;

        public LecturerDataService(TimetableManagerDbContext context)
        {
            _context = context;
        }

        public Task<int> AddLecturer(Lecturer lecturer, string fName, string dName, string cName, string bName, string lName)
        {
            SetLectureFields(lecturer, fName, dName, cName, bName, lName);

            return _context.SaveChangesAsync();
        }

        private void SetLectureFields(Lecturer lecturer, string fName, string dName, string cName, string bName, string lName)
        {
            var faculty = _context.Faculties.Include(e => e.Lecturers).Single(e => e.FacultyName == fName);
            faculty.Lecturers.Add(lecturer);

            var department = _context.Departments.Include(e => e.Lecturers).Single(e => e.DepartmentName == dName);
            department.Lecturers.Add(lecturer);

            var center = _context.Centers.Include(e => e.Lecturers).Single(e => e.CenterName == cName);
            center.Lecturers.Add(lecturer);

            var building = _context.Buildings.Include(e => e.Lecturers).Single(e => e.BuildingName == bName);
            building.Lecturers.Add(lecturer);

            var level = _context.Levels.Include(e => e.Lecturers).Single(e => e.LevelName == lName);
            level.Lecturers.Add(lecturer);
        }

        public async Task<List<Lecturer>> GetLecturersAsync()
        {
            return await _context.Lecturers
                                .Include(f => f.Faculty)
                                .Include(d => d.Department)
                                .Include(c => c.Center)
                                .Include(b => b.Building)
                                .Include(l => l.Level)
                                .ToListAsync();
        }

        public async Task<int> DeleteLecturer(int id)
        {
            var lecturer =  _context.Lecturers.Where(e => e.EmployeeId == id).First();

            _context.Lecturers.Remove(lecturer);

            return await _context.SaveChangesAsync();
        }

        public async Task<Lecturer> GetLectureById(int id)
        {
            return await _context.Lecturers.Where(e => e.EmployeeId == id)
                                .Include(e => e.Faculty)
                                .Include(e => e.Department)
                                .Include(e => e.Center)
                                .Include(e => e.Building)
                                .Include(e => e.Level)
                                .FirstAsync();
        }

        public async Task<int> UpdateLecturer(Lecturer lecturer, string fName, string dName, string cName, string bName, string lName)
        {
            await this.DeleteLecturer(lecturer.EmployeeId);

            SetLectureFields(lecturer, fName, dName, cName, bName, lName);

            return await _context.SaveChangesAsync();
        }
    }
}
