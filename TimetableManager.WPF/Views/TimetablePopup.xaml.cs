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
using System.Windows.Shapes;

namespace TimetableManager.WPF.Views
{
    /// <summary>
    /// Interaction logic for TimetablePopup.xaml
    /// </summary>
    public partial class TimetablePopup : Window
    {
        public ObservableCollection<string> DayTimeList { get; set; }
        private List<string> codedList;
        public string selectedTimeSlot;
        public TimetablePopup(List<string> dayTimeList)
        {
            InitializeComponent();
            DayTimeList = new ObservableCollection<string>();
            codedList = dayTimeList;
            string day;
            string startTime;
            string endTime;
            foreach (string slot in dayTimeList)
            {
                if (slot.Substring(0, 2) == "MO")
                {
                    day = "Monday";
                }
                else if (slot.Substring(0, 2) == "TU")
                {
                    day = "Tuesday";
                }
                else if (slot.Substring(0, 2) == "WE")
                {
                    day = "Wednesday";
                }
                else if (slot.Substring(0, 2) == "TH")
                {
                    day = "Thursday";
                }
                else if (slot.Substring(0, 2) == "FR")
                {
                    day = "Friday";
                }
                else if (slot.Substring(0, 2) == "SA")
                {
                    day = "Saturday";
                }
                else
                {
                    day = "Sunday";
                }
                startTime = slot.Substring(2, 4);
                endTime = slot.Substring(6, 4);
                DayTimeList.Add(day + " from " + startTime + " to " + endTime);
            }
            this.DataContext = this;
        }

        private void comboBoxIntervalSlot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedTimeSlot = e.AddedItems[0].ToString();
        }
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("Selected " + codedList[DayTimeList.IndexOf(selectedTimeSlot)]);
            this.Close();
        }
    }
}
