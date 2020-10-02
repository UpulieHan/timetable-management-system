using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.Domain.Models
{
    public class GroupIdPreferredRoom
    {
        public int GroupId { get; set; }
        public GroupId Group { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
