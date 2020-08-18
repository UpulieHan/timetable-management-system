using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimetableManager.Domain.Models;

namespace TimetableManager.EntityFramework.Services
{
    public class LecturerDataService
    {
        private readonly TimetableManagerDbContext _context;

        public LecturerDataService(TimetableManagerDbContext context)
        {
            _context = context;
        }

        public void AddLecturer(Lecturer lecturer)
        {
           
        }
    }
}
