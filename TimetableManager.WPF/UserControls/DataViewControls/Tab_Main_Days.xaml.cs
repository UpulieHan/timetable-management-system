using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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
        private bool stackPanelBorderCreated;

        private TimetableManagerDbContext timetableManagerDbContext = new TimetableManagerDbContext();
        public Tab_Main_Days()
        {
            InitializeComponent();

            stackPanelBorderCreated = false;

            //retrieving the DaysAndHours data from the DB if exists
            try
            {
                //setting the selected Days list to an ObservableCollection so it could be bound to the view
                theDaysList = new ObservableCollection<Day>(timetableManagerDbContext.Days);
                selectedDaysList = new ObservableCollection<string>();

                //the DataService for Days table
                DayDataService dayDataService = new DayDataService(new EntityFramework.TimetableManagerDbContext());

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
                if (theDaysList != null)
                {
                    foreach (Day day in theDaysList)
                    {
                        if (day.IsSelected == true)
                        {
                            createStackPanelBorder();
                            createStackPanel(day.DayName, day.IsSelected, day.startHour, day.startMin, day.endHour, day.endMin);
                        }
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
            createStackPanel((string)((CheckBox)sender).Content, false, null, null, null, null);
        }

        private void comboBoxDay_Unchecked(object sender, EventArgs e)
        {
            selectedDaysList.Remove((string)((CheckBox)sender).Content);
            //daysStackPanel.Children.Clear();
            StackPanel sp = (StackPanel)daysStackPanel.FindName("daySubPanel" + (string)((CheckBox)sender).Content);

            if (sp != null)
            {
                daysStackPanel.Children.Remove(sp);

                //need to unregister the name so i can use it again later
                UnregisterName(sp.Name);
            }
        }

        //no need of this method
        private void comboBoxDay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //this is not a combo box 
            Trace.WriteLine("comboBoxDay_SelectionChanged");
        }
        private void comboBoxStartHours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //get the parent
            StackPanel sp = VisualTreeHelper.GetParent(sender as ComboBox) as StackPanel;
            //get the sibling through parent
            Label label = sp.Children[0] as Label;

            //getting the day from the DB
            Day day = theDaysList.Where(predicate: d => d.DayName == (string)label.Content).FirstOrDefault();

            day.startHour = (sender as ComboBox).SelectedItem as string;
            timetableManagerDbContext.Days.Update(day);
            timetableManagerDbContext.SaveChanges();
        }
        private void comboBoxStartMins_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //get the parent
            StackPanel sp = VisualTreeHelper.GetParent(sender as ComboBox) as StackPanel;
            //get the sibling through parent
            Label label = sp.Children[0] as Label;

            //getting the day from the DB
            Day day = theDaysList.Where(predicate: d => d.DayName == (string)label.Content).FirstOrDefault();

            day.startMin = (sender as ComboBox).SelectedItem as string;
            timetableManagerDbContext.Days.Update(day);
            timetableManagerDbContext.SaveChanges();
        }
        private void comboBoxEndHours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //get the parent
            StackPanel sp = VisualTreeHelper.GetParent(sender as ComboBox) as StackPanel;
            //get the sibling through parent
            Label label = sp.Children[0] as Label;

            //getting the day from the DB
            Day day = theDaysList.Where(predicate: d => d.DayName == (string)label.Content).FirstOrDefault();

            day.endHour = (sender as ComboBox).SelectedItem as string;
            timetableManagerDbContext.Days.Update(day);
            timetableManagerDbContext.SaveChanges();
        }
        private void comboBoxEndMins_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //get the parent
            StackPanel sp = VisualTreeHelper.GetParent(sender as ComboBox) as StackPanel;
            //get the sibling through parent
            Label label = sp.Children[0] as Label;

            //getting the day from the DB
            Day day = theDaysList.Where(predicate: d => d.DayName == (string)label.Content).FirstOrDefault();

            day.endMin = (sender as ComboBox).SelectedItem as string;
            timetableManagerDbContext.Days.Update(day);
            timetableManagerDbContext.SaveChanges();
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

                    //creating the dayTimeCodes
                    createDayTimeCodes();

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

            //the selected no of days
            foreach (var item in theDaysList)
            {

                if (item.IsSelected)
                {
                    selectedNoOfDays++;

                    //checking if the duration matches the start and end times of the days
                    int modifiedEndHour = Int32.Parse(item.endHour);
                    if (Int32.Parse(item.endMin) - Int32.Parse(item.startMin) < 0)
                    {
                        modifiedEndHour = (Int32.Parse(item.endHour) - 1);
                    }
                    if (mins == 60)
                    {
                        mins = 0;
                    }
                    if ((hours != (modifiedEndHour - Int32.Parse(item.startHour))) || (mins != Math.Abs(Int32.Parse(item.endMin) - Int32.Parse(item.startMin))))
                    {
                        if (mins == 0)
                        {
                            mins = 60;
                        }
                        return false;
                    }
                    if (mins == 0)
                    {
                        mins = 60;
                    }
                }
            }

            //if time slot is 60 mins and working duration is a multiplication of 30 mins
            if (timeSlot == 60 && mins == 30)
            {
                return false;
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

        private void createDayTimeCodes()
        {
            List<string> dayTimeCodeList = new List<string>();

            foreach (var item in theDaysList)
            {
                if (item.IsSelected == true)
                {
                    //MO TU WE TH FR SA
                    //day part
                    string day = item.DayName.Substring(0, 2).ToUpper();

                    //breaking the day into slots of desired timeslot
                    int minTime = Int32.Parse(item.startMin);
                    if (timeSlot.Equals(30))
                    {
                        //break by 30 min slots
                        for (int hourTime = Int32.Parse(item.startHour); hourTime < Int32.Parse(item.endHour);)
                        {
                            if (minTime == 00)
                            {
                                dayTimeCodeList.Add(day + (hourTime).ToString().PadLeft(2, '0') + "00" + (hourTime).ToString().PadLeft(2, '0') + "30");
                                minTime = 30;
                            }
                            if (minTime == 30)
                            {
                                dayTimeCodeList.Add(day + (hourTime).ToString().PadLeft(2, '0') + "30" + (hourTime + 1).ToString().PadLeft(2, '0') + "00");
                                minTime = 00;
                                hourTime++;

                                //when endMin is 30
                                if (hourTime == Int32.Parse(item.endHour) && Int32.Parse(item.endMin) == 30)
                                {
                                    dayTimeCodeList.Add(day + (hourTime).ToString().PadLeft(2, '0') + "00" + (hourTime).ToString().PadLeft(2, '0') + "30");
                                }
                            }
                        }


                    }
                    else
                    {
                        //break by 60 min slots
                        for (int hourTime = Int32.Parse(item.startHour); hourTime < Int32.Parse(item.endHour); hourTime++)
                        {
                            //if startMin is 30 (both startMin and endMin become 30 here)
                            if (Int32.Parse(item.startMin) == 30)
                            {
                                dayTimeCodeList.Add(day + hourTime.ToString().PadLeft(2, '0') + "30" + (hourTime + 1).ToString().PadLeft(2, '0') + "30");
                            }
                            else
                            {
                                dayTimeCodeList.Add(day + hourTime.ToString().PadLeft(2, '0') + "00" + (hourTime + 1).ToString().PadLeft(2, '0') + "00");
                            }
                        }
                    }
                }
            }

            //save the list on the DB?
            foreach (string dayTimeCode in dayTimeCodeList)
            {
                Trace.WriteLine(dayTimeCode);
            }
        }
        private void createStackPanelBorder()
        {
            if (!stackPanelBorderCreated)
            {
                daysSPLabel.Visibility = Visibility.Visible;
                daysSPLabel.Margin = new Thickness(20, 10, 0, 0);
                daysSPBorder.BorderThickness = new Thickness(1);
            }
        }
        private void createStackPanel(string dayName, bool isSelected, string startHour, string startMin, string endHour, string endMin)
        {
            List<string> hourList = new List<string>() { "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23" };

            List<string> minList = new List<string>() { "00", "30" };

            StackPanel sp = new StackPanel
            {
                Name = "daySubPanel" + dayName,
                Orientation = Orientation.Horizontal,

            };

            //need to register the control so i can find it by name
            RegisterName(sp.Name, sp);

            //comboBoxDays
            Label comboBoxDays = new Label
            {
                Name = "comboBoxDays",
                Margin = new Thickness(20, 10, 0, 10),
                Padding = new Thickness(10),
                Width = 130,
                Content = dayName
            };

            //setting starthour text 
            if (startHour == null)
            {
                startHour = "Start Hour";
            }

            //comboBoxStartHour
            ComboBox comboBoxStartHour = new ComboBox
            {
                Name = "comboBoxStartHour",
                Margin = new Thickness(20, 10, 0, 10),
                Padding = new Thickness(10),
                Width = 130,
                IsEditable = true,
                IsReadOnly = false,
                Text = startHour,
            };
            comboBoxStartHour.SelectionChanged += comboBoxStartHours_SelectionChanged;
            comboBoxStartHour.ItemsSource = hourList;

            //setting startmin text 
            if (startMin == null)
            {
                startMin = "Start Minutes";
            }

            //comboBoxStartMin
            ComboBox comboBoxStartMin = new ComboBox
            {
                Name = "comboBoxStartMin",
                Margin = new Thickness(20, 10, 0, 10),
                Padding = new Thickness(10),
                Width = 150,
                IsEditable = true,
                IsReadOnly = false,
                Text = startMin,
            };
            comboBoxStartMin.SelectionChanged += comboBoxStartMins_SelectionChanged;
            comboBoxStartMin.ItemsSource = minList;

            //setting endhour text 
            if (endHour == null)
            {
                endHour = "End Hour";
            }

            //comboBoxEndHour
            ComboBox comboBoxEndHour = new ComboBox
            {
                Name = "comboBoxEndHour",
                Margin = new Thickness(20, 10, 0, 10),
                Padding = new Thickness(10),
                Width = 150,
                IsEditable = true,
                IsReadOnly = false,
                Text = endHour,
            };
            comboBoxEndHour.SelectionChanged += comboBoxEndHours_SelectionChanged;
            comboBoxEndHour.ItemsSource = hourList;


            //setting endhour text 
            if (endMin == null)
            {
                endMin = "End Minutes";
            }

            //comboBoxEndMin
            ComboBox comboBoxEndMin = new ComboBox
            {
                Name = "comboBoxEndMin",
                Margin = new Thickness(20, 10, 0, 10),
                Padding = new Thickness(10),
                Width = 150,
                IsEditable = true,
                IsReadOnly = false,
                Text = endMin,
            };
            comboBoxEndMin.SelectionChanged += comboBoxEndMins_SelectionChanged;
            comboBoxEndMin.ItemsSource = minList;


            sp.Children.Add(comboBoxDays);
            sp.Children.Add(comboBoxStartHour);
            sp.Children.Add(comboBoxStartMin);
            sp.Children.Add(comboBoxEndHour);
            sp.Children.Add(comboBoxEndMin);
            daysStackPanel.Children.Add(sp);
        }
    }
}
//loopholes, if the user closes the window after entering a not matching value, it's stored in the table (but an error will occur once the user tries to save again)
