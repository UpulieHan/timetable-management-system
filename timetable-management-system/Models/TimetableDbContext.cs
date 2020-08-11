using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using timetable_management_system.Config;

namespace timetable_management_system.Models
{
    class TimetableDbContext : DbContext
    {
        public DbSet<Faculty> Faculties { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbConfig.getConnectionString());
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.HasKey(e => e.FacultyId);
                entity.Property(e => e.FacultyName);
            });
        }
    }
}
