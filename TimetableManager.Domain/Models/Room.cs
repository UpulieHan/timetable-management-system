namespace TimetableManager.Domain.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int Capacity { get; set; }
        public Building Building { get; set; }
        public Center Center { get; set; }
        public List<TagPreferredRoom> TagPreferredRooms { get; set; }
        public List<SubjectPreferredRoom> SubjectPreferredRooms { get; set; }
        public List<LecturerPreferredRoom> LecturerPreferredRooms { get; set; }
        public List<SessionPreferredRoom> SessionPreferredRooms { get; set; }
        public List<GroupIdPreferredRoom> GroupIdPreferredRooms { get; set; }
        public List<SubGroupIdPrefferedRoom> SubGroupIdPrefferedRooms { get; set; }
    }
}
