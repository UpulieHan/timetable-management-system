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

namespace TimetableManager.WPF.Controls
{
    /// <summary>
    /// Interaction logic for Tab_Main_Days.xaml
    /// </summary>
    public partial class Tab_Main_Days : UserControl
    {
        public Tab_Main_Days()
        {
            InitializeComponent();
        }

        private void comboBoxNoOfDays_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //int selectedIndex = comboBoxNoOfDays.SelectedIndex;
            //Object selectedItem = comboBoxNoOfDays.SelectedValue;
            //textBox.Text = selectedItem.ToString();

        }
        private void comboBoxHours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //int selectedIndex = comboBoxNoOfDays.SelectedIndex;
            //Object selectedItem = comboBoxNoOfDays.SelectedValue;
            //textBox.Text = selectedItem.ToString();

        }
        private void comboBoxMinutes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //int selectedIndex = comboBoxNoOfDays.SelectedIndex;
            //Object selectedItem = comboBoxNoOfDays.SelectedValue;
            //textBox.Text = selectedItem.ToString();

        }

    }
}
