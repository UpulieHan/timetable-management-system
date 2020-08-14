using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.EntityFramework.Services
{
    public class IDataService
    {
        protected readonly TimetableManagerDbContext _context;

        public IDataService(TimetableManagerDbContext context)
        {
            _context = context;
        }
    }
}
