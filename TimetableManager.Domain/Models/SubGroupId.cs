using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.Domain.Models
{
    public class SubGroupId
    {
        public int Id { get; set; }
        public string SubGroupID { get; set; }
        public List<SubGroupIdSession> SubGroupIdSessions { get; set; }
        public List<SubGroupIdUnavailableTimeSlot> SubGroupIdUnavailableTimeSlots { get; set; }
        public List<SubGroupIdPrefferedRoom> SubGroupIdPrefferedRooms { get; set; }
    }
}
