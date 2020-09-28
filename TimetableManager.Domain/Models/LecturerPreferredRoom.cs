using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.Domain.Models
{
    public class LecturerPreferredRoom
    {
        public int LectuererId { get; set; }
        public Lecturer Lectuer { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
