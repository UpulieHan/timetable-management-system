using System;
using System.Collections.Generic;
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

namespace TimetableManager.WPF.StatisticsTimetableDataControls.StatisticUserControl
{
    /// <summary>
    /// Interaction logic for Tab_Stat_Student.xaml
    /// </summary>
    public partial class Tab_Stat_Student : UserControl
    {
        public Tab_Stat_Student()
        {
            InitializeComponent();
            List<Std> std = new List<Std>();

            std.Add(new Std() { programme="common", year = "1st year", subgroups = 8 });
            std.Add(new Std() { programme = "common",  year = "2st year", subgroups = 7 });
            std.Add(new Std() { programme = "SE", year = "3st year", subgroups = 3 });
            std.Add(new Std() { programme = "SE", year = "4st year", subgroups = 2 });
            std.Add(new Std() { programme = "ISE", year = "3st year", subgroups = 2 });
            std.Add(new Std() { programme = "ISE", year = "4st year", subgroups = 1 });
            std.Add(new Std() { programme = "IT", year = "3st year", subgroups = 3 });
            std.Add(new Std() { programme = "IT", year = "4st year", subgroups = 2 });
            dataGridstd.ItemsSource = std;

        }

        private void button_Click_std(object sender, RoutedEventArgs e)
        {

        }
    }

    internal class Std
    {

        public string programme { get; set; }
        public string year { get; set; }

        public int subgroups { get; set; }
    }
}
