using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.Domain.Models
{
    public class SubGroupIdPrefferedRoom
    {
        public int SubGroupId { get; set; }
        public SubGroupId SubGroup { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
