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
    /// Interaction logic for Tab_Stat_lecturer.xaml
    /// </summary>
    public partial class Tab_Stat_lecturer : UserControl
    {
        public Tab_Stat_lecturer()
        {
            InitializeComponent();
            List<Lec> lec = new List<Lec>();

            lec.Add(new Lec() { rank = "Prof", number =2 });
            lec.Add(new Lec() { rank = "Senior Lec", number = 4 });
            lec.Add(new Lec() { rank = "Hod", number = 5 });
            lec.Add(new Lec() { rank = "Lecturer", number = 30 });
            lec.Add(new Lec() { rank = "Assistant", number = 40 });
            dataGrid.ItemsSource = lec;

        }


       /* public class lec
        {
            public string rank { get; set; }

            public int number { get; set; }

            public string Details { get {

                    return String.Format("", rank, number);
                
                } }

        }*/
    }

    internal class Lec
    {
        public string rank { get; set; }

        public int number { get; set; }

     /*   public string Details
        {
            get
            {

                return String.Format( rank, number);

            }
        }*/
    }
}
