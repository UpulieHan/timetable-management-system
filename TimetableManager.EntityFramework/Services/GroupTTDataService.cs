using Microsoft.Data.SqlClient;
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
    public class GroupTTDataService
    {
        private readonly TimetableManagerDbContext _context;

        public GroupTTDataService(TimetableManagerDbContext context)
        {
            _context = context;
        }

    }
}
