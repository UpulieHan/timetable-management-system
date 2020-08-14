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
        private int noOfDays = 0;
        private int selectedNoOfDays;
        private int hours;
        private int mins;
        private int timeSlot;

        private TimetableManagerDbContext timetableManagerDbContext = new TimetableManagerDbContext();
        public Tab_Main_Days()
        {
            InitializeComponent();

            //setting the selected Days list to an ObservableCollection so it could be bound to the view
            theDaysList = new ObservableCollection<Day>(timetableManagerDbContext.Days);
            this.DataContext = this;
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
            mins = Int32.Parse(text);
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

                //saving data to the DB
                DaysAndHoursDataService daysAndHoursDataService = new DaysAndHoursDataService(new EntityFramework.TimetableManagerDbContext());

                //check from the GenericDataService what to pass to this (T entity)
                //changed the int? to int
                DaysAndHours daysAndHours = new DaysAndHours();
                //daysAndHours.Id=
                daysAndHours.NoOfDays = noOfDays;
                daysAndHours.Hours = hours;
                daysAndHours.Mins = mins;
                daysAndHours.TimeSlot = timeSlot;

                //there's no such think as .Create() we must implement it.
                //daysAndHoursDataService.Create();







                MessageBox.Show("All good");
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
