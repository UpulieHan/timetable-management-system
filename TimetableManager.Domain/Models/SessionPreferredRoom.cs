using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.Domain.Models
{
    public class SessionPreferredRoom
    {
        public int SessionId { get; set; }
        public Session Session { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
