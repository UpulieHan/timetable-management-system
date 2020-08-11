using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.EntityFramework
{
    public class TimetableManagerDbContextFactory : IDesignTimeDbContextFactory<TimetableManagerDbContext>
    {
        public TimetableManagerDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<TimetableManagerDbContext>();
            options.UseSqlServer("Server=database-1.cmxg05ztpr3u.us-east-1.rds.amazonaws.com,1433;Database=timetableManagerDb;User Id=admin;Password=test1234;");

            return new TimetableManagerDbContext(options.Options);
        }
    }
}
