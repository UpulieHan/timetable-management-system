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
                            createStackPanel(day.DayName);
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
            createStackPanel((string)((CheckBox)sender).Content);
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

                    //theDaysList is already created (will always be not null)


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

            //the selected no of days
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
        private void createStackPanelBorder()
        {
            if (!stackPanelBorderCreated)
            {
                daysSPLabel.Visibility = Visibility.Visible;
                daysSPLabel.Margin = new Thickness(20, 10, 0, 0);
                daysSPBorder.BorderThickness = new Thickness(1);
            }
        }
        private void createStackPanel(string dayName)
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

            //comboBoxStartHour
            ComboBox comboBoxStartHour = new ComboBox
            {
                Name = "comboBoxStartHour",
                Margin = new Thickness(20, 10, 0, 10),
                Padding = new Thickness(10),
                Width = 130,
                IsEditable = true,
                IsReadOnly = false,
                Text = "Start Hour",
            };
            comboBoxStartHour.SelectionChanged += comboBoxStartHours_SelectionChanged;
            comboBoxStartHour.ItemsSource = hourList;


            //comboBoxStartMin
            ComboBox comboBoxStartMin = new ComboBox
            {
                Name = "comboBoxStartMin",
                Margin = new Thickness(20, 10, 0, 10),
                Padding = new Thickness(10),
                Width = 150,
                IsEditable = true,
                IsReadOnly = false,
                Text = "Start Minutes",
            };
            comboBoxStartMin.SelectionChanged += comboBoxStartMins_SelectionChanged;
            comboBoxStartMin.ItemsSource = minList;


            //comboBoxEndHour
            ComboBox comboBoxEndHour = new ComboBox
            {
                Name = "comboBoxEndHour",
                Margin = new Thickness(20, 10, 0, 10),
                Padding = new Thickness(10),
                Width = 150,
                IsEditable = true,
                IsReadOnly = false,
                Text = "End Hour",
            };
            comboBoxEndHour.SelectionChanged += comboBoxEndHours_SelectionChanged;
            comboBoxEndHour.ItemsSource = hourList;

            //comboBoxEndMin
            ComboBox comboBoxEndMin = new ComboBox
            {
                Name = "comboBoxEndMin",
                Margin = new Thickness(20, 10, 0, 10),
                Padding = new Thickness(10),
                Width = 150,
                IsEditable = true,
                IsReadOnly = false,
                Text = "End Minutes",
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
