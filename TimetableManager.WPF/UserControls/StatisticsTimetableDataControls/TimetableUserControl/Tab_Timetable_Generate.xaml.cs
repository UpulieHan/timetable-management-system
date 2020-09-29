using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace TimetableManager.WPF.StatisticsTimetableDataControls.TimetableUserControl
{
    public partial class Tab_Timetable_Generate : UserControl
    {
        public Tab_Timetable_Generate()
        {
            InitializeComponent();
        }

        private void generateButton_Click(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("generateButton click");
        }
    }
}
