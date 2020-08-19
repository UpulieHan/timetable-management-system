using System;
using System.Collections.Generic;
using System.Text;

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
