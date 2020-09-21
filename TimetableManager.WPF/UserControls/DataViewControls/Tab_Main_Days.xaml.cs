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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimetableManager.Domain.Models;
using TimetableManager.EntityFramework;
using TimetableManager.EntityFramework.Services;

namespace TimetableManager.WPF.Controls
{
    /// <summary>
    /// Interaction logic for Tab_Main_Days.xaml
    /// </summary>
    public partial class Tab_Main_Days : UserControl
    {
        public ObservableCollection<Day> theDaysList { get; set; }
        public ObservableCollection<string> selectedDaysList { get; set; }
        private DaysAndHours daysAndHours;
        private int noOfDays = 0;
        private int selectedNoOfDays;
        private int hours;
        private int mins;
        private int timeSlot;

        private TimetableManagerDbContext timetableManagerDbContext = new TimetableManagerDbContext();
        public Tab_Main_Days()
        {
            InitializeComponent();

            //retrieving the DaysAndHours data from the DB if exists
            try
            {
                //setting the selected Days list to an ObservableCollection so it could be bound to the view
                theDaysList = new ObservableCollection<Day>(timetableManagerDbContext.Days);
                selectedDaysList = new ObservableCollection<string>();

                //setting the rest of the saved data
                daysAndHours = timetableManagerDbContext.DaysAndHours.FirstOrDefault<DaysAndHours>();

                if (daysAndHours != null)
                {
                    noOfDays = daysAndHours.NoOfDays;
                    hours = daysAndHours.Hours;
                    mins = daysAndHours.Mins;
                    timeSlot = daysAndHours.TimeSlot;

                    //setting data to the view
                    comboBoxNoOfDays.SelectedIndex = noOfDays;
                    comboBoxHours.SelectedIndex = hours;

                    if (mins.Equals(30))
                    {
                        comboBoxMinutes.Text = "30";
                    }
                    else if (mins.Equals(60))
                    {
                        comboBoxMinutes.Text = "00";
                    }

                    if (timeSlot.Equals(30))
                    {
                        comboBoxDuration.Text = "30 mins";
                    }
                    else if (timeSlot.Equals(60))
                    {
                        comboBoxDuration.Text = "1 hour";
                    }

                }
                this.DataContext = this;

            }
            catch (Exception e)
            {
                //in case if the connection to the DB is lost
                MessageBox.Show("first error " + e.Message);
            }


        }
        private void comboBoxDay_Checked(object sender, EventArgs e)
        {
            selectedDaysList.Add((string)((CheckBox)sender).Content);
            foreach (string s in selectedDaysList)
            {
                Trace.WriteLine(s);
            }
        }

        private void comboBoxDay_Unchecked(object sender, EventArgs e)
        {
            selectedDaysList.Remove((string)((CheckBox)sender).Content);
            foreach (string s in selectedDaysList)
            {
                Trace.WriteLine(s);
            }
        }

        private void comboBoxDay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Trace.WriteLine("comboBoxDay_SelectionChanged");
        }
        private void comboBoxStartHours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Trace.WriteLine("comboBoxStartHours_SelectionChanged");
        }
        private void comboBoxStartMins_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Trace.WriteLine("comboBoxStartMins_SelectionChanged");
        }
        private void comboBoxEndHours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Trace.WriteLine("comboBoxEndHours_SelectionChanged");
        }
        private void comboBoxEndMins_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Trace.WriteLine("comboBoxEndMins_SelectionChanged");
        }

        private void comboBoxNoOfDays_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (e.AddedItems[0] as ComboBoxItem).Content.ToString();
            noOfDays = Int32.Parse(text);
        }

        private void comboBoxHours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (e.AddedItems[0] as ComboBoxItem).Content.ToString();
            hours = Int32.Parse(text);
        }
        private void comboBoxMinutes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (e.AddedItems[0] as ComboBoxItem).Content.ToString();

            if (text.Equals("00"))
            {
                mins = 60;
            }
            else if (text.Equals("30"))
            {
                mins = 30;
            }
        }

        private void comboBoxDuration_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (e.AddedItems[0] as ComboBoxItem).Content.ToString();

            if (text.Equals("30 mins"))
            {
                timeSlot = 30;
            }
            else if (text.Equals("1 hour"))
            {
                timeSlot = 60;
            }
        }

        private void mouseEnter_Event(object sender, EventArgs e)
        {
            checkDateValidity();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            bool ret = checkDateValidity();

            if (ret && (hours != 0) && (timeSlot != 0))
            {
                var uriSource = new Uri("/Resources/Save_tick.png", UriKind.Relative);
                tickImage.Source = new BitmapImage(uriSource);

                //Adding the daysAndHours object to the DB
                try
                {

                    if (daysAndHours != null)
                    {
                        daysAndHours.NoOfDays = noOfDays;
                        daysAndHours.Hours = hours;
                        daysAndHours.Mins = mins;
                        daysAndHours.TimeSlot = timeSlot;

                        timetableManagerDbContext.DaysAndHours.Update(daysAndHours);
                    }
                    else
                    {
                        daysAndHours = new DaysAndHours();
                        daysAndHours.NoOfDays = noOfDays;
                        daysAndHours.Hours = hours;
                        daysAndHours.Mins = mins;
                        daysAndHours.TimeSlot = timeSlot;

                        timetableManagerDbContext.DaysAndHours.Add(daysAndHours);
                    }
                    timetableManagerDbContext.SaveChanges();
                    MessageBox.Show("Changes saved");
                }
                catch (Exception ex)
                {
                    //in case if the connection to the DB is lost
                    MessageBox.Show("second error" + ex.Message);
                }
            }
            else
            {
                var uriSource = new Uri("/Resources/Cancel_tick.png", UriKind.Relative);
                tickImage.Source = new BitmapImage(uriSource);
                MessageBox.Show("Not all values match. Please try again");
            }

        }

        private bool checkDateValidity()
        {
            selectedNoOfDays = 0;

            //the selected no of days days
            foreach (var item in theDaysList)
            {

                if (item.IsSelected)
                {
                    selectedNoOfDays++;
                }
            }

            //the number of days
            if (selectedNoOfDays == noOfDays)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
