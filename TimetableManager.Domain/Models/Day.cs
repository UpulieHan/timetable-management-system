namespace TimetableManager.Domain.Models
{
    public class Day
    {
        public int DayId { get; set; }
        public string DayName { get; set; }
        public bool IsSelected { get; set; }
        public string startHour { get; set; }
        public string startMin { get; set; }
        public string endHour { get; set; }
        public string endMin { get; set; }
    }
}
