using System.Collections.Generic;

namespace TimetableManager.Domain.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public Faculty Faculty { get; set; }
        public List<Lecturer> Lecturers { get; set; }
    }
}
