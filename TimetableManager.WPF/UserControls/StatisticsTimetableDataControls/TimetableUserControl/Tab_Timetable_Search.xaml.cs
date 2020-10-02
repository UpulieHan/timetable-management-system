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
using TimetableManager.EntityFramework;
using TimetableManager.WPF.Views;

namespace TimetableManager.WPF.StatisticsTimetableDataControls.TimetableUserControl
{
    public partial class Tab_Timetable_Search : UserControl
    {
        public ObservableCollection<string> comboBoxValueList;
        private ObservableCollection<Lecturer> theLecturerList { get; set; }
        private ObservableCollection<GroupId> theGroupList { get; set; }
        private ObservableCollection<SubGroupId> theSubGroupList { get; set; }
        private ObservableCollection<Room> theRoomList { get; set; }

        private TimetableManagerDbContext timetableManagerDbContext = new TimetableManagerDbContext();
        public Tab_Timetable_Search()
        {
            InitializeComponent();
            comboBoxValueList = new ObservableCollection<string>();

            try
            {
                theLecturerList = new ObservableCollection<Lecturer>(timetableManagerDbContext.Lecturers);
                theGroupList = new ObservableCollection<GroupId>(timetableManagerDbContext.GroupIds);
                theSubGroupList = new ObservableCollection<SubGroupId>(timetableManagerDbContext.SubGroupIds);
                theRoomList = new ObservableCollection<Room>(timetableManagerDbContext.Rooms);
            }
            catch (Exception e)
            {
                //in case if the connection to the DB is lost
                MessageBox.Show("first error " + e.Message);
            }
            this.DataContext = this;

        }

        private void comboBoxTimetableType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBoxSpecificName.ItemsSource = null;
            string selectedValue = (e.AddedItems[0] as ComboBoxItem).Content.ToString();

            //set comboBocValueList accordingly
            if (selectedValue == "Lecturer")
            {
                comboBoxValueList.Clear();
                foreach (Lecturer l in theLecturerList)
                {
                    comboBoxValueList.Add(l.EmployeeName);
                }
            }
            else if (selectedValue == "Group")
            {
                comboBoxValueList.Clear();
                foreach (GroupId g in theGroupList)
                {
                    comboBoxValueList.Add(g.GroupID);
                }
                foreach (SubGroupId s in theSubGroupList)
                {
                    comboBoxValueList.Add(s.SubGroupID);
                }
            }
            else if (selectedValue == "Room")
            {
                comboBoxValueList.Clear();
                foreach (Room r in theRoomList)
                {
                    comboBoxValueList.Add(r.RoomName);
                }
            }

            comboBoxSpecificName.ItemsSource = comboBoxValueList;
            comboBoxSpecificName.Items.Refresh();
        }
        private void comboBoxSpecificName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxSpecificName.ItemsSource != null)
            {
                string selectedValue = comboBoxSpecificName.SelectedItem.ToString();
                //Trace.WriteLine(selectedValue);
            }
        }


        private void viewButton_Click(object sender, RoutedEventArgs e)
        {
            TimetablePopup timetablePopup = new TimetablePopup();
            timetablePopup.Show();
        }
    }
}
