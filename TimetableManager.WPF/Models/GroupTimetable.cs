using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.Domain.Models
{
    public class GroupTimetable
    {
        public int Id { get; set; }
        public string groupId { get; set; }
        public string timeSlot { get; set; }
        public string subject { get; set; }
        public string lecturer { get; set; }
        public string room { get; set; }
    }
}
