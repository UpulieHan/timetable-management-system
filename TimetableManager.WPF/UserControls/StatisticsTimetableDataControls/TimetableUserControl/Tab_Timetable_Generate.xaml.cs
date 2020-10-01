using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimetableManager.Domain.Models;
using TimetableManager.EntityFramework;

namespace TimetableManager.WPF.StatisticsTimetableDataControls.TimetableUserControl
{
    public partial class Tab_Timetable_Generate : UserControl
    {
        private TimetableManagerDbContext timetableManagerDbContext = new TimetableManagerDbContext();
        private DaysAndHours theDaysAndHours { get; set; }
        private ObservableCollection<Day> theDaysList { get; set; }
        private ObservableCollection<Session> theSessionsList { get; set; }
        private ObservableCollection<GroupId> theGroupList { get; set; }
        private ObservableCollection<SubGroupId> theSubGroupList { get; set; }
        private ObservableCollection<Lecturer> theLecturerList { get; set; }
        private ObservableCollection<Room> theRoomList { get; set; }

        public Tab_Timetable_Generate()
        {
            InitializeComponent();

            //fetching data from the db
            theDaysAndHours = timetableManagerDbContext.DaysAndHours.FirstOrDefault<DaysAndHours>();
            theDaysList = new ObservableCollection<Day>(timetableManagerDbContext.Days);
            theSessionsList = new ObservableCollection<Session>(timetableManagerDbContext.Sessions);
            theGroupList = new ObservableCollection<GroupId>(timetableManagerDbContext.GroupIds);
            theSubGroupList = new ObservableCollection<SubGroupId>(timetableManagerDbContext.SubGroupIds);
            theLecturerList = new ObservableCollection<Lecturer>(timetableManagerDbContext.Lecturers);
            theRoomList = new ObservableCollection<Room>(timetableManagerDbContext.Rooms);

            this.DataContext = this;
        }

        private void generateButton_Click(object sender, RoutedEventArgs e)
        {

            //create e folder (inside timetable-management-system\TimetableManager.WPF\bin\Debug\netcoreapp3.1\) to save timetable if doesn't exist (& 3 sub folders)
            DirectoryInfo timetablesDir = new DirectoryInfo("timetablesDir");

            // Create the directory only if it does not already exist.
            if (timetablesDir.Exists == false)
                timetablesDir.Create();

            // Create a subdirectories in the directory just created.
            DirectoryInfo lecturerDir = timetablesDir.CreateSubdirectory("lecturerDir");
            DirectoryInfo groupDir = timetablesDir.CreateSubdirectory("groupDir");
            DirectoryInfo RoomDir = timetablesDir.CreateSubdirectory("RoomDir");




            //a binary field to say if the timetable is being made for all 3 types

            //lecturers
            foreach (Lecturer l in theLecturerList)
            {
                Trace.WriteLine(l.EmployeeName);
            }


            //locations
            foreach (Room r in theRoomList)
            {
                Trace.WriteLine(r.RoomName);
            }

            //groups
            foreach (GroupId g in theGroupList)
            {
                Trace.WriteLine(g.GroupID);
            }

            foreach (SubGroupId s in theSubGroupList)
            {
                Trace.WriteLine(s.SubGroupID);
            }

            //set the title, cols (days), rows(timeslots)

        }
    }
}
