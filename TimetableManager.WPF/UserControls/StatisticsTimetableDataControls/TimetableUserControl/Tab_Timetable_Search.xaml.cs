using System;
using System.Collections.Generic;
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
using TimetableManager.WPF.Views;

namespace TimetableManager.WPF.StatisticsTimetableDataControls.TimetableUserControl
{
    public partial class Tab_Timetable_Search : UserControl
    {
        public Tab_Timetable_Search()
        {
            InitializeComponent();
        }

        private void comboBoxTimetableType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Trace.WriteLine("comboBoxTimetableType clicked");
        }
        private void comboBoxSpecificName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Trace.WriteLine("comboBoxSpecificName clicked");
        }


        private void viewButton_Click(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("view Button clicked");
            TimetablePopup timetablePopup = new TimetablePopup();
            timetablePopup.Show();
        }

        private void printButton_Click(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("print Button clicked");

        }
    }
}
