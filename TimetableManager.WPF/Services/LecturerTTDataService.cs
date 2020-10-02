using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableManager.Domain.Models;

namespace TimetableManager.EntityFramework.Services
{
    public class LecturerTTDataService
    {
        private readonly TimetableManagerDbContext _context;

        public LecturerTTDataService(TimetableManagerDbContext context)
        {
            _context = context;
        }

    }
}
