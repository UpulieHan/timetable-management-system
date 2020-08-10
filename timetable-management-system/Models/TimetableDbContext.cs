using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using timetable_management_system.Config;

namespace timetable_management_system.Models
{
    class TimetableDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbConfig.getConnectionString());
            base.OnConfiguring(optionsBuilder);
        }
    }
}
