using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.Domain.Models
{
    public class SubGroupIdUnavailableTimeSlot
    {
        public int SubGroupId { get; set; }
        public SubGroupId SubGroup { get; set; }
        public string TimeSlotId { get; set; }
        public TimeSlot TimeSlot { get; set; }
    }
}
