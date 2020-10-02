using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.Domain.Models
{
    public class LecturerSession
    {
        public int LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }
        public int SessionId { get; set; }
        public Session Session { get; set; }
    }
}
