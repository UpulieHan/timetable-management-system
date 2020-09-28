using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.Domain.Models
{
    public class LecturerUnavailableTimeSlot
    {
        public int LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }
        public string TimeSlotId { get; set; }
        public TimeSlot TimeSlot { get; set; }
    }
}
