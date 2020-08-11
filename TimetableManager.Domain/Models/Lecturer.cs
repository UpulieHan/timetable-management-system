using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.Domain.Models
{
    public class Lecturer : DomainObject
    {
        public string EmployeeName { get; set; }
        public Faculty Faculty { get; set; }
        public Department Department { get; set; }
        public Center Center { get; set; }
        public Building Building { get; set; }
        public Level Level { get; set; }
        public float Rank { get; set; }
    }
}
