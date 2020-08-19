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
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Year_Semester> Year_Semesters { get; set; }
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<GroupNumber> GroupNumbers { get; set; }
        public DbSet<SubGroupNumber> SubGroupNumbers { get; set; }

        public TimetableManagerDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=database-1.cmxg05ztpr3u.us-east-1.rds.amazonaws.com,1433;Database=timetableDb;User Id=admin;Password=test1234;");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lecturer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasAlternateKey(e => e.EmployeeId);
            });

            modelBuilder.Entity<Level>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasAlternateKey(e => e.LevelId);
            });

            // Add Sample Center Data
            modelBuilder.Entity<Center>().HasData(GetCenterList());

            // Add Sample Level Data
            modelBuilder.Entity<Level>().HasData(GetLevelList());

            // Add Sample Faculty Data
            modelBuilder.Entity<Faculty>().HasData(GetFacultyList());

            base.OnModelCreating(modelBuilder);
        }

        private Center[] GetCenterList()
        {
            return new Center[]
            {
                new Center { CenterId = 1, CenterName = "Malabe" },
                new Center { CenterId = 2, CenterName = "Matara" },
                new Center { CenterId = 3, CenterName = "Kandy" },
            };
        }

        private Level[] GetLevelList()
        {
            return new Level[]
            {
                new Level { Id = 1, LevelId = 1, LevelName = "Professor"},
                new Level { Id = 2, LevelId = 2, LevelName = "Assistant Professor"},
                new Level { Id = 3, LevelId = 3, LevelName = "Senior Lecturer(HG)"},
                new Level { Id = 4, LevelId = 4, LevelName = "Senior Lecturer"},
                new Level { Id = 5, LevelId = 5, LevelName = "Lecturer"},
                new Level { Id = 6, LevelId = 6, LevelName = "Assistant Lecturer"},
                new Level { Id = 7, LevelId = 7, LevelName = "Instructors"}
            };
        }

        private Faculty[] GetFacultyList()
        {
            return new Faculty[]
            {
                new Faculty { FacultyId = 1, FacultyName = "Computing"},
                new Faculty { FacultyId = 2, FacultyName = "Engineering"},
                new Faculty { FacultyId = 3, FacultyName = "Business"}
            };
        }
    }
}