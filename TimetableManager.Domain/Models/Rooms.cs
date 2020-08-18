using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.Domain.Models
{
    class Rooms
    {
        public int RoomId { get; set; }

        public string room { get; set; }

        public string capacity { get; set; }

        public Building building { get; set; }

        public Center center { get; set; }
    }
}
