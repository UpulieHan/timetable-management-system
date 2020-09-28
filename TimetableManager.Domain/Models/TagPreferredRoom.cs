using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.Domain.Models
{
    public class TagPreferredRoom
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
