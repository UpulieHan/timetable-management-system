using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TimetableManager.Domain.Models;

namespace TimetableManager.EntityFramework
{
    public class TimetableManagerDbContext : DbContext
    {
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Center> Centers { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Level> Levels { get; set; }

        public TimetableManagerDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lecturer>()
                .HasKey(l => l.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}