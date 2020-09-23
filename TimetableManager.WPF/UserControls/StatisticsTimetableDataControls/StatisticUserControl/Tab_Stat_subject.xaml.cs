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
    /// Interaction logic for Tab_Stat_subject.xaml
    /// </summary>
    public partial class Tab_Stat_subject : UserControl
    {
        public Tab_Stat_subject()
        {
            InitializeComponent();
            List<Sub> sub = new List<Sub>();

            sub.Add(new Sub() { module = "AF", hours = 25 });
            sub.Add(new Sub() { module = "CN", hours = 30 });
            sub.Add(new Sub() { module = "SA", hours = 30 });
            sub.Add(new Sub() { module = "DS", hours = 25 });
            dataGridstd.ItemsSource = sub;
        }

        private void button_Click_sub(object sender, RoutedEventArgs e)
        {

        }
    }

    internal class Sub
    {
        public string module { get; set; }

        public int hours { get; set; }

    }
}
