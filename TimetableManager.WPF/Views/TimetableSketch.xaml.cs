using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TimetableManager.Domain.Models;
using TimetableManager.EntityFramework;

namespace TimetableManager.WPF.Views
{
    /// <summary>
    /// Interaction logic for TimetableSketch.xaml

    public partial class TimetableSketch : Window
    {
        private TimetableManagerDbContext timetableManagerDbContext = new TimetableManagerDbContext();
        private DaysAndHours theDaysAndHours { get; set; }
        private ObservableCollection<TimeSlot> timeSlotList { get; set; }
        private ObservableCollection<Lecturer> lecturerList { get; set; }
        private ObservableCollection<Session> sessionList { get; set; }
        private ObservableCollection<GroupId> groupList { get; set; }
        private ObservableCollection<Room> roomList { get; set; }
        private ObservableCollection<Day> daysList { get; set; }
        private IEnumerable<LecturerSession> lecturerSessionList { get; set; }
        public string WindowTitle { get; }
        private Lecturer lecturer;
        private GroupId group;
        private Room room;
        private string prefix;
        private int noOfRows;

        public TimetableSketch(string type, string id)
        {
            InitializeComponent();

            theDaysAndHours = timetableManagerDbContext.DaysAndHours.FirstOrDefault<DaysAndHours>();
            timeSlotList = new ObservableCollection<TimeSlot>(timetableManagerDbContext.TimeSlots);
            daysList = new ObservableCollection<Day>(timetableManagerDbContext.Days);

            //adding row headers
            IEnumerable<string> distinctTimeSlots = timeSlotList.Select(x => x.startTime).Distinct().OrderBy(o => o);

            noOfRows = distinctTimeSlots.Count();

            //set window titile
            if (type == "Lecturer")
            {
                lecturerList = new ObservableCollection<Lecturer>(timetableManagerDbContext.Lecturers);

                foreach (Lecturer l in lecturerList)
                {
                    if (l.EmployeeName == id)
                    {
                        lecturer = l;
                    }
                }
                if (lecturer.Id == 1)
                {
                    prefix = "Professor";
                }
                if (lecturer.Id == 2)
                {
                    prefix = "Assistant Professor";
                }
                if (lecturer.Id == 3)
                {
                    prefix = "Senior Lecturer(HG)";
                }
                if (lecturer.Id == 4)
                {
                    prefix = "Senior Lecturer";
                }
                if (lecturer.Id == 5)
                {
                    prefix = "Lecturer";
                }
                if (lecturer.Id == 6)
                {
                    prefix = "Assistant Lecturer";
                }
                if (lecturer.Id == 7)
                {
                    prefix = "Instructor";
                }
                WindowTitle = prefix + " " + lecturer.EmployeeName;
            }
            else if (type == "Group")
            {
                groupList = new ObservableCollection<GroupId>(timetableManagerDbContext.GroupIds);

                foreach (GroupId g in groupList)
                {
                    if (g.GroupID == id)
                    {
                        group = g;
                    }
                }
                WindowTitle = group.GroupID;
            }
            else if (type == "Room")
            {
                roomList = new ObservableCollection<Room>(timetableManagerDbContext.Rooms);

                foreach (Room r in roomList)
                {
                    if (r.RoomName == id)
                    {
                        room = r;
                    }
                }
                WindowTitle = room.RoomName;
            }


            //adding rows
            for (int i = 0; i < noOfRows + 1; i++)
            {
                RowDefinition gridRow = new RowDefinition();
                gridRow.Height = new GridLength(30);
                mainGrid.RowDefinitions.Add(gridRow);
            }

            //adding cols
            for (int i = 0; i < 8; i++)
            {
                ColumnDefinition gridCols = new ColumnDefinition();
                gridCols.Width = new GridLength(130);
                mainGrid.ColumnDefinitions.Add(gridCols);
            }

            // Add column headers
            foreach (Day d in daysList)
            {
                Label day = new Label
                {
                    Name = d.DayName,
                    Content = d.DayName,
                    Margin = new Thickness(0, 3, 0, 3),
                    FontSize = 11,
                };
                day.FontWeight = FontWeights.Bold;

                Grid.SetRow(day, 0);
                Grid.SetColumn(day, d.DayId);

                mainGrid.Children.Add(day);
            }

            int count = 0;
            foreach (string s in distinctTimeSlots)
            {
                Trace.WriteLine(s);
                Label time = new Label
                {
                    Name = "Time" + s,
                    Content = s,
                    Margin = new Thickness(0, 3, 0, 3),
                    FontSize = 11,
                };
                time.FontWeight = FontWeights.Bold;
                count++;
                Grid.SetRow(time, count);
                Grid.SetColumn(time, 0);
                mainGrid.Children.Add(time);
            }


            if (type == "Lecturer")
            {
                //createLecturerTT(lecturer);
            }
            else if (type == "Group")
            {
                createGroupTT(id);
            }
            else if (type == "Room")
            {
                createRoomTT(id);
            }




            this.DataContext = this;

            //find the particular timetable and display it


        }

        private void printButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(timetableGrid, "Timetable");
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }
        private void createLecturerTT(Lecturer l)
        {
            for (int c = 1; c < 9; c++)
            {
                for (int r = 1; r < noOfRows + 1; r++)
                {

                    //lecturerSessionList.has

                    foreach (LecturerSession ls in lecturerSessionList)
                    {
                        Trace.WriteLine(ls.Lecturer.EmployeeName);
                        Trace.WriteLine(ls.Session.SessionId);
                    }
                    //set these
                    string subjectName = "IT2030 - OOP";
                    string groupName = "Y2S1.03(IT)";
                    string locationName = "A507";

                    StackPanel sp = new StackPanel
                    {
                        Name = "spLecturer" + c + r,
                        Orientation = Orientation.Vertical,
                    };

                    //need to register the control so i can find it by name
                    RegisterName(sp.Name, sp);

                    Label subject = new Label
                    {
                        Name = "subject",
                        Content = subjectName,
                        Margin = new Thickness(0, -2, 0, -5),
                        FontSize = 8,
                    };

                    Label group = new Label
                    {
                        Name = "group",
                        Content = groupName,
                        Margin = new Thickness(0, -5, 0, -5),
                        FontSize = 8,
                    };

                    Label location = new Label
                    {
                        Name = "location",
                        Content = locationName,
                        Margin = new Thickness(0, -5, 0, -2),
                        FontSize = 8,
                    };

                    sp.Children.Add(subject);
                    sp.Children.Add(group);
                    sp.Children.Add(location);

                    Grid.SetRow(sp, r);
                    Grid.SetColumn(sp, c);

                    mainGrid.Children.Add(sp);
                }
            }
        }
        private void createRoomTT(string id)
        {
            for (int c = 1; c < 9; c++)
            {
                for (int r = 1; r < noOfRows + 1; r++)
                {
                    //set these
                    string subjectName = "IT2030 - OOP";
                    string groupName = "Y2S1.03(IT)";
                    string lecturerName = "Dr.Nuwan Kodagoda";

                    StackPanel sp = new StackPanel
                    {
                        Name = "spRoom" + c + r,
                        Orientation = Orientation.Vertical,
                    };

                    //need to register the control so i can find it by name
                    RegisterName(sp.Name, sp);

                    Label subject = new Label
                    {
                        Name = "subject",
                        Content = subjectName,
                        Margin = new Thickness(0, -2, 0, -5),
                        FontSize = 8,
                    };

                    Label group = new Label
                    {
                        Name = "group",
                        Content = groupName,
                        Margin = new Thickness(0, -5, 0, -5),
                        FontSize = 8,
                    };

                    Label lecturer = new Label
                    {
                        Name = "location",
                        Content = lecturerName,
                        Margin = new Thickness(0, -5, 0, -2),
                        FontSize = 8,
                    };

                    sp.Children.Add(subject);
                    sp.Children.Add(group);
                    sp.Children.Add(lecturer);

                    Grid.SetRow(sp, r);
                    Grid.SetColumn(sp, c);

                    mainGrid.Children.Add(sp);
                }
            }
        }
        private void createGroupTT(string id)
        {
            for (int c = 1; c < 9; c++)
            {
                for (int r = 1; r < noOfRows + 1; r++)
                {
                    //set these
                    string subjectName = "IT2030 - OOP";
                    string lecturerName = "Dr. Nuwan Kodagoda";
                    string locationName = "A507";

                    StackPanel sp = new StackPanel
                    {
                        Name = "spGroup" + c + r,
                        Orientation = Orientation.Vertical,
                    };

                    //need to register the control so i can find it by name
                    RegisterName(sp.Name, sp);

                    Label subject = new Label
                    {
                        Name = "subject",
                        Content = subjectName,
                        Margin = new Thickness(0, -2, 0, -5),
                        FontSize = 8,
                    };

                    Label lecturer = new Label
                    {
                        Name = "group",
                        Content = lecturerName,
                        Margin = new Thickness(0, -5, 0, -5),
                        FontSize = 8,
                    };

                    Label location = new Label
                    {
                        Name = "location",
                        Content = locationName,
                        Margin = new Thickness(0, -5, 0, -2),
                        FontSize = 8,
                    };

                    sp.Children.Add(subject);
                    sp.Children.Add(lecturer);
                    sp.Children.Add(location);

                    Grid.SetRow(sp, r);
                    Grid.SetColumn(sp, c);

                    mainGrid.Children.Add(sp);
                }
            }
        }
    }
}



