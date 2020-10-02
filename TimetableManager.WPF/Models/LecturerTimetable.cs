using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.Domain.Models
{
    public class LecturerTimetable
    {
        public int Id { get; set; }
        public string lecturer { get; set; }
        public string TimeSlot { get; set; }
        public string subjectName { get; set; }
        public string groupId { get; set; }
        public string room { get; set; }
    }
}
