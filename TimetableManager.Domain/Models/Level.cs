using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.Domain.Models
{
    public class Level
    {
        public int Id { get; set; }
        public int LevelId { get; set; }
        public string LevelName { get; set; }
        public List<Lecturer> Lecturers { get; set; }
    }
}
