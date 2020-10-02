using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.Domain.Models
{
    public class RoomTimetable
    {
        public int Id { get; set; }
        public string roomName { get; set; }
        public string timeSlot { get; set; }
        public string subject { get; set; }
        public string groupId { get; set; }
        public string lecturer { get; set; }
    }
}
