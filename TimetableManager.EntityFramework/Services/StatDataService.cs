using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimetableManager.Domain.Models;

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
