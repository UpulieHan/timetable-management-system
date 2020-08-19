﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimetableManager.EntityFramework;

namespace TimetableManager.EntityFramework.Migrations
{
    [DbContext(typeof(TimetableManagerDbContext))]
    [Migration("20200819211833_ClutterFreeMigration")]
    partial class ClutterFreeMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TimetableManager.Domain.Models.Building", b =>
                {
                    b.Property<int>("BuildingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BuildingName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CenterId")
                        .HasColumnType("int");

                    b.HasKey("BuildingId");

                    b.HasIndex("CenterId");

                    b.ToTable("Buildings");
                });

            modelBuilder.Entity("TimetableManager.Domain.Models.Center", b =>
                {
                    b.Property<int>("CenterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CenterName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CenterId");

                    b.ToTable("Centers");

                    b.HasData(
                        new
                        {
                            CenterId = 1,
                            CenterName = "Malabe"
                        },
                        new
                        {
                            CenterId = 2,
                            CenterName = "Matara"
                        },
                        new
                        {
                            CenterId = 3,
                            CenterName = "Kandy"
                        });
                });

            modelBuilder.Entity("TimetableManager.Domain.Models.Day", b =>
                {
                    b.Property<int>("DayId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSelected")
                        .HasColumnType("bit");

                    b.HasKey("DayId");

                    b.ToTable("Days");

                    b.HasData(
                        new
                        {
                            DayId = 1,
                            DayName = "Monday",
                            IsSelected = false
                        },
                        new
                        {
                            DayId = 2,
                            DayName = "Tuesday",
                            IsSelected = false
                        },
                        new
                        {
                            DayId = 3,
                            DayName = "Wednesday",
                            IsSelected = false
                        },
                        new
                        {
                            DayId = 4,
                            DayName = "Thursday",
                            IsSelected = false
                        },
                        new
                        {
                            DayId = 5,
                            DayName = "Friday",
                            IsSelected = false
                        },
                        new
                        {
                            DayId = 6,
                            DayName = "Saturday",
                            IsSelected = false
                        },
                        new
                        {
                            DayId = 7,
                            DayName = "Sunday",
                            IsSelected = false
                        });
                });

            modelBuilder.Entity("TimetableManager.Domain.Models.DaysAndHours", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Hours")
                        .HasColumnType("int");

                    b.Property<int>("Mins")
                        .HasColumnType("int");

                    b.Property<int>("NoOfDays")
                        .HasColumnType("int");

                    b.Property<int>("TimeSlot")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DaysAndHours");
                });

            modelBuilder.Entity("TimetableManager.Domain.Models.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DepartmentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FacultyId")
                        .HasColumnType("int");

                    b.HasKey("DepartmentId");

                    b.HasIndex("FacultyId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("TimetableManager.Domain.Models.Faculty", b =>
                {
                    b.Property<int>("FacultyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FacultyName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FacultyId");

                    b.ToTable("Faculties");

                    b.HasData(
                        new
                        {
                            FacultyId = 1,
                            FacultyName = "Computing"
                        },
                        new
                        {
                            FacultyId = 2,
                            FacultyName = "Engineering"
                        },
                        new
                        {
                            FacultyId = 3,
                            FacultyName = "Business"
                        });
                });

            modelBuilder.Entity("TimetableManager.Domain.Models.GroupNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GroupNum")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GroupNumbers");
                });

            modelBuilder.Entity("TimetableManager.Domain.Models.Lecturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BuildingId")
                        .HasColumnType("int");

                    b.Property<int?>("CenterId")
                        .HasColumnType("int");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("EmployeeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FacultyId")
                        .HasColumnType("int");

                    b.Property<int?>("LevelId")
                        .HasColumnType("int");

                    b.Property<float>("Rank")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasAlternateKey("EmployeeId");

                    b.HasIndex("BuildingId");

                    b.HasIndex("CenterId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("FacultyId");

                    b.HasIndex("LevelId");

                    b.ToTable("Lecturers");
                });

            modelBuilder.Entity("TimetableManager.Domain.Models.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LevelId")
                        .HasColumnType("int");

                    b.Property<string>("LevelName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasAlternateKey("LevelId");

                    b.ToTable("Levels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            LevelId = 1,
                            LevelName = "Professor"
                        },
                        new
                        {
                            Id = 2,
                            LevelId = 2,
                            LevelName = "Assistant Professor"
                        },
                        new
                        {
                            Id = 3,
                            LevelId = 3,
                            LevelName = "Senior Lecturer(HG)"
                        },
                        new
                        {
                            Id = 4,
                            LevelId = 4,
                            LevelName = "Senior Lecturer"
                        },
                        new
                        {
                            Id = 5,
                            LevelId = 5,
                            LevelName = "Lecturer"
                        },
                        new
                        {
                            Id = 6,
                            LevelId = 6,
                            LevelName = "Assistant Lecturer"
                        },
                        new
                        {
                            Id = 7,
                            LevelId = 7,
                            LevelName = "Instructors"
                        });
                });

            modelBuilder.Entity("TimetableManager.Domain.Models.Programme", b =>
                {
                    b.Property<int>("ProgrammeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProgrammeFullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProgrammeShortName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProgrammeId");

                    b.ToTable("Programmes");
                });

            modelBuilder.Entity("TimetableManager.Domain.Models.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BuildingId")
                        .HasColumnType("int");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int?>("CenterId")
                        .HasColumnType("int");

                    b.Property<string>("RoomName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoomId");

                    b.HasIndex("BuildingId");

                    b.HasIndex("CenterId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("TimetableManager.Domain.Models.SubGroupNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SubGroupNum")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SubGroupNumbers");
                });

            modelBuilder.Entity("TimetableManager.Domain.Models.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TagName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("TimetableManager.Domain.Models.Year_Semester", b =>
                {
                    b.Property<int>("YsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("YsSemester")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("YsShortName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("YsYear")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("YsId");

                    b.ToTable("Year_Semesters");
                });

            modelBuilder.Entity("TimetableManager.Domain.Models.Building", b =>
                {
                    b.HasOne("TimetableManager.Domain.Models.Center", "Center")
                        .WithMany("Buildings")
                        .HasForeignKey("CenterId");
                });

            modelBuilder.Entity("TimetableManager.Domain.Models.Department", b =>
                {
                    b.HasOne("TimetableManager.Domain.Models.Faculty", "Faculty")
                        .WithMany("Departments")
                        .HasForeignKey("FacultyId");
                });

            modelBuilder.Entity("TimetableManager.Domain.Models.Lecturer", b =>
                {
                    b.HasOne("TimetableManager.Domain.Models.Building", "Building")
                        .WithMany("Lecturers")
                        .HasForeignKey("BuildingId");

                    b.HasOne("TimetableManager.Domain.Models.Center", "Center")
                        .WithMany("Lecturers")
                        .HasForeignKey("CenterId");

                    b.HasOne("TimetableManager.Domain.Models.Department", "Department")
                        .WithMany("Lecturers")
                        .HasForeignKey("DepartmentId");

                    b.HasOne("TimetableManager.Domain.Models.Faculty", "Faculty")
                        .WithMany("Lecturers")
                        .HasForeignKey("FacultyId");

                    b.HasOne("TimetableManager.Domain.Models.Level", "Level")
                        .WithMany("Lecturers")
                        .HasForeignKey("LevelId");
                });

            modelBuilder.Entity("TimetableManager.Domain.Models.Room", b =>
                {
                    b.HasOne("TimetableManager.Domain.Models.Building", "Building")
                        .WithMany("Rooms")
                        .HasForeignKey("BuildingId");

                    b.HasOne("TimetableManager.Domain.Models.Center", "Center")
                        .WithMany("Rooms")
                        .HasForeignKey("CenterId");
                });
#pragma warning restore 612, 618
        }
    }
}
