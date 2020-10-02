using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.Domain.Models
{
    public class SessionUnavailableTimeSlot
    {
        public int SessionId { get; set; }
        public Session Session { get; set; }
        public string TimeSlotId { get; set; }
        public TimeSlot TimeSlot { get; set; }
    }
}
