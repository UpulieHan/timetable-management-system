﻿namespace TimetableManager.Domain.Models
{
    public class TimeSlot
    {
        public string CodeId { get; set; }
        public string DayName { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string sessionId { get; set; }
    }
}
