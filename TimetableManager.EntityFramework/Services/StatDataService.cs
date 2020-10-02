namespace TimetableManager.EntityFramework.Services
{
    public class StatDataService
    {
        private readonly TimetableManagerDbContext _context;

        public StatDataService(TimetableManagerDbContext context)
        {
            _context = context;
        }
    }
}
