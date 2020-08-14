using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using TimetableManager.EntityFramework.Services;

namespace TimetableManager.WPF.Controls
{
    /// <summary>
    /// Interaction logic for Tab_Main_Days.xaml
    /// </summary>
    public partial class Tab_Main_Days : UserControl
    {
        private int noOfDays = 0;
        private int selectedNoOfDays;
        private int? hours;
        private int? mins;
        private int? timeSlot;
        public Tab_Main_Days()
        {
            InitializeComponent();

            //setting the selected Days list
            //DayList = new ObservableCollection<Day>();

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

        //
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            bool ret = checkDateValidity();

            if (ret && (hours != null) && (hours != 0) && (mins != null) && (timeSlot != null) && (timeSlot != 0))
            {
                var uriSource = new Uri("/Resources/Save_tick.png", UriKind.Relative);
                tickImage.Source = new BitmapImage(uriSource);


                //creating a service of DaysAndHours using IDataService interface
                //IDataService<DaysAndHours> daysAndHoursDataService = new GenericDataService<DaysAndHours>(new EntityFramework.TimetableManagerDbContext());




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
            //foreach (var item in DayList)
            //{

            //    if (item.IsSelected)
            //    {
            //        Trace.WriteLine(item.DayName);
            //        Trace.WriteLine("Now selected no of days is " + item.DayName);
            //        selectedNoOfDays++;
            //    }
            //}

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
