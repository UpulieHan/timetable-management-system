using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.Domain.Models
{
    public class GroupIdUnavailableTimeSlot
    {
        public int GroupId { get; set; }
        public GroupId Group { get; set; }
        public string TimeSlotId { get; set; }
        public TimeSlot TimeSlot { get; set; }
    }
}
