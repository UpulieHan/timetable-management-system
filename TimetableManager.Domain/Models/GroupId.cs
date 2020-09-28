using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.Domain.Models
{
    public class GroupId
    {
        public int Id { get; set; }
        public string GroupID { get; set; }
        public List<GroupIdSession> GroupIdSessions { get; set; }
        public List<GroupIdUnavailableTimeSlot> GroupIdUnavailableTimeSlots { get; set; }
        public List<GroupIdPreferredRoom> GroupIdPreferredRooms { get; set; }
    }
}
