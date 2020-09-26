using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.Domain.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public Subject Subject { get; set; }
        public Tag Tag { get; set; }
        public int StudentCount { get; set; }
        public int Duration { get; set; }
        public List<LecturerSession> LecturerSessions { get; set; }
        public List<GroupIdSession> GroupIdSessions { get; set; }
        public List<SubGroupIdSession> SubGroupIdSessions { get; set; }
    }
}
