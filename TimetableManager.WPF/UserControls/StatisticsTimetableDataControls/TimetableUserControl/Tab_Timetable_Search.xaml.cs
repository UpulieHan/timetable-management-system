using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

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

        }

        private void printButton_Click(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("print Button clicked");
        }
    }
}
