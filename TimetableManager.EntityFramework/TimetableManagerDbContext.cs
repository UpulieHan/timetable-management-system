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
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Year_Semester> Year_Semesters { get; set; }
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<GroupNumber> GroupNumbers { get; set; }
        public DbSet<SubGroupNumber> SubGroupNumbers { get; set; }

        //upulie's
        public DbSet<Day> Days { get; set; }
        public DbSet<DaysAndHours> DaysAndHours { get; set; }
        public DbSet<GroupId> GroupIds { get; set; }
        public DbSet<SubGroupId> SubGroupIds { get; set; }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }

        public DbSet<Session> Sessions { get; set; }
        public TimetableManagerDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=database-1.cmxg05ztpr3u.us-east-1.rds.amazonaws.com,1433;Database=timetableDb;User Id=admin;Password=test1234;");
            optionsBuilder.UseSqlite("Filename=D:/Developement/uni-projects/timetable-management-system/TimetableManager.EntityFramework/Timetable.db");
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

            //upulies
            modelBuilder.Entity<Day>(entity =>
            {
                entity.HasKey(e => e.DayId);
                entity.HasAlternateKey(e => e.DayName);
            });

            modelBuilder.Entity<Day>().HasData(GetDayList());

            modelBuilder.Entity<DaysAndHours>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NoOfDays);
                entity.Property(e => e.Hours);
                entity.Property(e => e.Mins);
                entity.Property(e => e.TimeSlot);
            });

            modelBuilder.Entity<TimeSlot>(entity =>
            {
                entity.HasKey(e => e.CodeId);
                entity.Property(e => e.DayName);
                entity.Property(e => e.startTime);
                entity.Property(e => e.endTime);
                entity.Property(e => e.sessionId);
            });

            modelBuilder.Entity<LecturerSession>()
                .HasKey(t => new { t.LecturerId, t.SessionId });

            modelBuilder.Entity<LecturerSession>()
                .HasOne(ls => ls.Lecturer)
                .WithMany(l => l.LecturerSessions)
                .HasForeignKey(ls => ls.LecturerId);

            modelBuilder.Entity<LecturerSession>()
                .HasOne(ls => ls.Session)
                .WithMany(s => s.LecturerSessions)
                .HasForeignKey(ls => ls.SessionId);

            modelBuilder.Entity<GroupIdSession>()
                .HasKey(e => new { e.GroupId, e.SessionId });

            modelBuilder.Entity<GroupIdSession>()
                .HasOne(gs => gs.Group)
                .WithMany(g => g.GroupIdSessions)
                .HasForeignKey(gs => gs.GroupId);

            modelBuilder.Entity<GroupIdSession>()
                .HasOne(gs => gs.Session)
                .WithMany(s => s.GroupIdSessions)
                .HasForeignKey(gs => gs.SessionId);
            
            modelBuilder.Entity<SubGroupIdSession>()
                .HasKey(e => new { e.SubGroupId, e.SessionId });

            modelBuilder.Entity<SubGroupIdSession>()
                .HasOne(gs => gs.SubGroup)
                .WithMany(g => g.SubGroupIdSessions)
                .HasForeignKey(gs => gs.SubGroupId);

            modelBuilder.Entity<SubGroupIdSession>()
                .HasOne(gs => gs.Session)
                .WithMany(s => s.SubGroupIdSessions)
                .HasForeignKey(gs => gs.SessionId);

            modelBuilder.Entity<LecturerUnavailableTimeSlot>()
                .HasKey(e => new { e.LecturerId, e.TimeSlotId });

            modelBuilder.Entity<LecturerUnavailableTimeSlot>()
                .HasOne(l => l.Lecturer)
                .WithMany(t => t.LecturerUnavailableTimeSlots)
                .HasForeignKey(ts => ts.LecturerId);

            modelBuilder.Entity<LecturerUnavailableTimeSlot>()
                .HasOne(t => t.TimeSlot)
                .WithMany(t => t.LecturerUnavailableTimeSlots)
                .HasForeignKey(ts => ts.TimeSlotId);

            modelBuilder.Entity<SessionUnavailableTimeSlot>()
                .HasKey(e => new { e.SessionId, e.TimeSlotId });

            modelBuilder.Entity<SessionUnavailableTimeSlot>()
                .HasOne(s => s.Session)
                .WithMany(t => t.SessionUnavailableTimeSlots)
                .HasForeignKey(sts => sts.SessionId);

            modelBuilder.Entity<SessionUnavailableTimeSlot>()
                .HasOne(t => t.TimeSlot)
                .WithMany(ts => ts.SessionUnavailableTimeSlots)
                .HasForeignKey(sts => sts.TimeSlotId);

            modelBuilder.Entity<GroupIdUnavailableTimeSlot>()
                .HasKey(e => new { e.GroupId, e.TimeSlotId });

            modelBuilder.Entity<GroupIdUnavailableTimeSlot>()
                .HasOne(g => g.Group)
                .WithMany(t => t.GroupIdUnavailableTimeSlots)
                .HasForeignKey(gts => gts.GroupId);

            modelBuilder.Entity<GroupIdUnavailableTimeSlot>()
                .HasOne(t => t.TimeSlot)
                .WithMany(ts => ts.GroupIdUnavailableTimeSlots)
                .HasForeignKey(gts => gts.TimeSlotId);  
            
            modelBuilder.Entity<SubGroupIdUnavailableTimeSlot>()
                .HasKey(e => new { e.SubGroupId, e.TimeSlotId });

            modelBuilder.Entity<SubGroupIdUnavailableTimeSlot>()
                .HasOne(sg => sg.SubGroup)
                .WithMany(t => t.SubGroupIdUnavailableTimeSlots)
                .HasForeignKey(sgts => sgts.SubGroupId);

            modelBuilder.Entity<SubGroupIdUnavailableTimeSlot>()
                .HasOne(t => t.TimeSlot)
                .WithMany(ts => ts.SubGroupIdUnavailableTimeSlots)
                .HasForeignKey(sgts => sgts.TimeSlotId);

            modelBuilder.Entity<TagPreferredRoom>()
                .HasKey(e => new { e.TagId, e.RoomId });

            modelBuilder.Entity<TagPreferredRoom>()
                .HasOne(e => e.Tag)
                .WithMany(e => e.TagPreferredRooms)
                .HasForeignKey(e => e.TagId);

            modelBuilder.Entity<TagPreferredRoom>()
                .HasOne(e => e.Room)
                .WithMany(e => e.TagPreferredRooms)
                .HasForeignKey(e => e.RoomId);

            modelBuilder.Entity<SubjectPreferredRoom>()
                .HasKey(e => new { e.SubjectId, e.RoomId });

            modelBuilder.Entity<SubjectPreferredRoom>()
                .HasOne(e => e.Subject)
                .WithMany(e => e.SubjectPreferredRooms)
                .HasForeignKey(e => e.SubjectId);

            modelBuilder.Entity<SubjectPreferredRoom>()
                .HasOne(e => e.Room)
                .WithMany(e => e.SubjectPreferredRooms)
                .HasForeignKey(e => e.RoomId);

            modelBuilder.Entity<LecturerPreferredRoom>()
                .HasKey(e => new { e.LectuererId, e.RoomId });

            modelBuilder.Entity<LecturerPreferredRoom>()
                .HasOne(e => e.Lectuer)
                .WithMany(e => e.LecturerPreferredRooms)
                .HasForeignKey(e => e.LectuererId);

            modelBuilder.Entity<LecturerPreferredRoom>()
                .HasOne(e => e.Room)
                .WithMany(e => e.LecturerPreferredRooms)
                .HasForeignKey(e => e.RoomId);

            modelBuilder.Entity<SessionPreferredRoom>()
                .HasKey(e => new { e.SessionId, e.RoomId });

            modelBuilder.Entity<SessionPreferredRoom>()
                .HasOne(e => e.Session)
                .WithMany(e => e.SessionPreferredRooms)
                .HasForeignKey(e => e.SessionId);

            modelBuilder.Entity<SessionPreferredRoom>()
                .HasOne(e => e.Room)
                .WithMany(e => e.SessionPreferredRooms)
                .HasForeignKey(e => e.RoomId);

            modelBuilder.Entity<GroupIdPreferredRoom>()
                .HasKey(e => new { e.GroupId, e.RoomId });

            modelBuilder.Entity<GroupIdPreferredRoom>()
                .HasOne(e => e.Group)
                .WithMany(e => e.GroupIdPreferredRooms)
                .HasForeignKey(e => e.GroupId);

            modelBuilder.Entity<GroupIdPreferredRoom>()
                .HasOne(e => e.Room)
                .WithMany(e => e.GroupIdPreferredRooms)
                .HasForeignKey(e => e.RoomId);

            modelBuilder.Entity<SubGroupIdPrefferedRoom>()
                .HasKey(e => new { e.SubGroupId, e.RoomId });

            modelBuilder.Entity<SubGroupIdPrefferedRoom>()
                .HasOne(e => e.SubGroup)
                .WithMany(e => e.SubGroupIdPrefferedRooms)
                .HasForeignKey(e => e.SubGroupId);

            modelBuilder.Entity<SubGroupIdPrefferedRoom>()
                .HasOne(e => e.Room)
                .WithMany(e => e.SubGroupIdPrefferedRooms)
                .HasForeignKey(e => e.RoomId);

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
        private Day[] GetDayList()
        {
            return new Day[]
            {
                new Day { DayId= 1, DayName = "Monday",IsSelected = false,startHour=null,startMin=null,endHour=null,endMin=null},
                new Day { DayId= 2, DayName = "Tuesday",IsSelected = false,startHour=null,startMin=null,endHour=null,endMin=null},
                new Day { DayId= 3, DayName = "Wednesday",IsSelected = false,startHour=null,startMin=null,endHour=null,endMin=null},
                new Day { DayId= 4, DayName = "Thursday",IsSelected = false,startHour=null,startMin=null,endHour=null,endMin=null},
                new Day { DayId= 5, DayName = "Friday",IsSelected = false,startHour=null,startMin=null,endHour=null,endMin=null},
                new Day { DayId= 6, DayName = "Saturday",IsSelected = false,startHour=null,startMin=null,endHour=null,endMin=null},
                new Day { DayId= 7, DayName = "Sunday",IsSelected = false,startHour=null,startMin=null,endHour=null,endMin=null}

            };
        }
    }
}
