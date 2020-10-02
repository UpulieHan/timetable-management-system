namespace TimetableManager.Domain.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public List<Session> Sessions { get; set; }
        public List<TagPreferredRoom> TagPreferredRooms { get; set; }
    }
}
