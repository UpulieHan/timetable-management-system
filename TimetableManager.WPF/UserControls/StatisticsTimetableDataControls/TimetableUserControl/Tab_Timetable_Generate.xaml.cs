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

namespace TimetableManager.WPF.StatisticsTimetableDataControls.TimetableUserControl
{
    public partial class Tab_Timetable_Generate : UserControl
    {
        private TimetableManagerDbContext timetableManagerDbContext = new TimetableManagerDbContext();
        private ObservableCollection<Day> theDaysList { get; set; }
        private ObservableCollection<Session> theSessionsList { get; set; }
        private ObservableCollection<GroupId> theGroupList { get; set; }
        private ObservableCollection<SubGroupId> theSubGroupList { get; set; }
        private ObservableCollection<Room> theRoomList { get; set; }

        public Tab_Timetable_Generate()
        {
            InitializeComponent();
        }

        private void generateButton_Click(object sender, RoutedEventArgs e)
        {
            //set the title, cols (days), rows(timeslots)
            Trace.WriteLine("generateButton click");
        }
    }
}
