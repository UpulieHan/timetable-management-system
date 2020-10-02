using System.Collections.Generic;

namespace TimetableManager.Domain.Models
{
    public class Faculty
    {
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public List<Department> Departments { get; set; }
        public List<Lecturer> Lecturers { get; set; }
    }
}
