using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TimetableManager.Domain.Models;
using TimetableManager.EntityFramework;

namespace TimetableManager.WPF.Views
{
    /// <summary>
    /// Interaction logic for TimetablePopup.xaml

    public partial class TimetablePopup : Window
    {
        private TimetableManagerDbContext timetableManagerDbContext = new TimetableManagerDbContext();
        private DaysAndHours theDaysAndHours { get; set; }
        private ObservableCollection<TimeSlot> timeSlotList { get; set; }
        private ObservableCollection<Day> daysList { get; set; }
        public string WindowTitle { get; }

        public TimetablePopup()
        {
            InitializeComponent();

            theDaysAndHours = timetableManagerDbContext.DaysAndHours.FirstOrDefault<DaysAndHours>();
            timeSlotList = new ObservableCollection<TimeSlot>(timetableManagerDbContext.TimeSlots);
            daysList = new ObservableCollection<Day>(timetableManagerDbContext.Days);

            //adding row headers
            IEnumerable<string> distinctTimeSlots = timeSlotList.Select(x => x.startTime).Distinct().OrderBy(o => o);

            int noOfRows = distinctTimeSlots.Count();

            //set this
            WindowTitle = "Dr. Nuwan Kodagoda";

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

            for (int c = 1; c < 9; c++)
            {
                for (int r = 1; r < noOfRows + 1; r++)
                {
                    //set these
                    string subjectName = "IT2030 - OOP";
                    string groupName = "Y2S1.03(IT)";
                    string locationName = "A507";

                    StackPanel sp = new StackPanel
                    {
                        Name = "sp" + c + r,
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
    }
}



