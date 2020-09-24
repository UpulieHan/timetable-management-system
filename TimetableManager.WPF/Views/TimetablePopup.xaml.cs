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
    /// Interaction logic for TimetablePopup.xaml
    /// </summary>
    public partial class TimetablePopup : Window
    {
        public ObservableCollection<TimeSlot> theTimeSlotList { get; set; }
        public ObservableCollection<string> MondayTimeList { get; set; }
        public ObservableCollection<string> TuesdayTimeList { get; set; }
        public ObservableCollection<string> WednesdayTimeList { get; set; }
        public ObservableCollection<string> ThursdayTimeList { get; set; }
        public ObservableCollection<string> FridayTimeList { get; set; }
        public ObservableCollection<string> SaturdayTimeList { get; set; }
        public ObservableCollection<string> SundayTimeList { get; set; }
        public string selectedTimeSlot;
        private List<string> intervalList;
        private TimetableManagerDbContext timetableManagerDbContext = new TimetableManagerDbContext();
        public TimetablePopup(List<string> dayTimeList, List<string> intervalList)
        {
            this.intervalList = intervalList;
            InitializeComponent();
            List<ObservableCollection<string>> allLists = new List<ObservableCollection<string>>();
            MondayTimeList = new ObservableCollection<string>();
            TuesdayTimeList = new ObservableCollection<string>();
            WednesdayTimeList = new ObservableCollection<string>();
            ThursdayTimeList = new ObservableCollection<string>();
            FridayTimeList = new ObservableCollection<string>();
            SaturdayTimeList = new ObservableCollection<string>();
            SundayTimeList = new ObservableCollection<string>();

            string day;
            string startTime;
            string endTime;
            foreach (string slot in dayTimeList)
            {
                startTime = slot.Substring(2, 4);
                endTime = slot.Substring(6, 4);

                //adding to the according list accordingly
                if (slot.Substring(0, 2) == "MO")
                {
                    day = "Monday";
                    MondayTimeList.Add(day + " from " + startTime + " to " + endTime);
                }
                else if (slot.Substring(0, 2) == "TU")
                {
                    day = "Tuesday";
                    TuesdayTimeList.Add(day + " from " + startTime + " to " + endTime);
                }
                else if (slot.Substring(0, 2) == "WE")
                {
                    day = "Wednesday";
                    WednesdayTimeList.Add(day + " from " + startTime + " to " + endTime);
                }
                else if (slot.Substring(0, 2) == "TH")
                {
                    day = "Thursday";
                    ThursdayTimeList.Add(day + " from " + startTime + " to " + endTime);
                }
                else if (slot.Substring(0, 2) == "FR")
                {
                    day = "Friday";
                    FridayTimeList.Add(day + " from " + startTime + " to " + endTime);
                }
                else if (slot.Substring(0, 2) == "SA")
                {
                    day = "Saturday";
                    SaturdayTimeList.Add(day + " from " + startTime + " to " + endTime);
                }
                else
                {
                    day = "Sunday";
                    SundayTimeList.Add(day + " from " + startTime + " to " + endTime);
                }
            }
            //creating a list of lists
            if (MondayTimeList.Count > 0)
            {
                allLists.Add(MondayTimeList);
            }
            if (TuesdayTimeList.Count > 0)
            {
                allLists.Add(TuesdayTimeList);
            }
            if (WednesdayTimeList.Count > 0)
            {
                allLists.Add(WednesdayTimeList);
            }
            if (ThursdayTimeList.Count > 0)
            {
                allLists.Add(ThursdayTimeList);
            }
            if (FridayTimeList.Count > 0)
            {
                allLists.Add(FridayTimeList);
            }
            if (SaturdayTimeList.Count > 0)
            {
                allLists.Add(SaturdayTimeList);
            }
            if (SundayTimeList.Count > 0)
            {
                allLists.Add(SundayTimeList);
            }
            createComboBox(allLists);
            this.DataContext = this;
        }


        private void createComboBox(List<ObservableCollection<string>> allLists)
        {
            foreach (ObservableCollection<string> list in allLists)
            {
                //comboBoxInterval
                ComboBox comboBoxInterval = new ComboBox
                {
                    Name = "comboBoxInterval" + list[0].Substring(0, 2),
                    Margin = new Thickness(10, 0, 0, 0),
                    Padding = new Thickness(10),
                    Width = 250,
                    IsEditable = true,
                    IsReadOnly = false,
                    Text = list[0].Substring(0, 2),
                };
                comboBoxInterval.ItemsSource = list;


                foreach (string dayTimeCode in list)
                {
                    foreach (string interval in intervalList)
                    {
                        var words = dayTimeCode.Split(' ');
                        if (words[0].Substring(0, 2).ToUpper() + words[2] + words[4] == interval)
                        {
                            comboBoxInterval.Text = dayTimeCode;
                        }

                    }
                }
                comboboxSP.Children.Add(comboBoxInterval);


            }
        }
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            //retrieve values from the db
            theTimeSlotList = new ObservableCollection<TimeSlot>(timetableManagerDbContext.TimeSlots);

            bool selected = true;
            //retrieving the values from all the combo boxes
            foreach (ComboBox comboBox in comboboxSP.Children)
            {
                if (comboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Please fill all the fields");
                    selected = false;
                    break;
                }
                else
                {
                    var words = comboBox.Text.Split(' ');
                    TimeSlot matchedTimeSlot = timetableManagerDbContext.TimeSlots.Find(words[0].Substring(0, 2).ToUpper() + words[2] + words[4]);
                    //could change the name INTERVAL to something else in the future if needed.
                    matchedTimeSlot.sessionId = "INTERVAL";
                    timetableManagerDbContext.TimeSlots.Update(matchedTimeSlot);
                    timetableManagerDbContext.SaveChanges();
                }

            }
            if (selected)
            {
                //need to save this value somewhere
                this.Close();
            }
        }
    }
}
