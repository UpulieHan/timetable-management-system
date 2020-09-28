using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.Domain.Models
{
    public class SubjectPreferredRoom
    {
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
