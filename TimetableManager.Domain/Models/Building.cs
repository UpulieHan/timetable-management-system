using System.Collections.Generic;

namespace TimetableManager.Domain.Models
{
    public class Building
    {
        public int BuildingId { get; set; }
        public string BuildingName { get; set; }
        public Center Center { get; set; }
        public List<Lecturer> Lecturers { get; set; }

        public List<Room> Rooms { get; set; }
    }
}
