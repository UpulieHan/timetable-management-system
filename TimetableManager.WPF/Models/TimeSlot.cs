using System.Collections.Generic;

namespace TimetableManager.Domain.Models
{
    public class TimeSlot
    {
        public string CodeId { get; set; }
        public string DayName { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string sessionId { get; set; }
        public List<LecturerUnavailableTimeSlot> LecturerUnavailableTimeSlots { get; set; }
        public List<SessionUnavailableTimeSlot> SessionUnavailableTimeSlots { get; set; }
        public List<GroupIdUnavailableTimeSlot> GroupIdUnavailableTimeSlots { get; set; }
        public List<SubGroupIdUnavailableTimeSlot> SubGroupIdUnavailableTimeSlots { get; set; }
    }
}
