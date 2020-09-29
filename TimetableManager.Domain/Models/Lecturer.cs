namespace TimetableManager.Domain.Models
{
    public class Lecturer
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public Faculty Faculty { get; set; }
        public Department Department { get; set; }
        public Center Center { get; set; }
        public Building Building { get; set; }
        public Level Level { get; set; }
        public float Rank { get; set; }
    }
}
