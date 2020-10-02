using System.Collections.Generic;

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
