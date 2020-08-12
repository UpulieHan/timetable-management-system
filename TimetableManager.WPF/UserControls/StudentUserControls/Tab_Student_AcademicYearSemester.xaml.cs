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

namespace TimetableManager.WPF.UserControls.StudentUserControls
{
    /// <summary>
    /// Interaction logic for Tab_Student_AcademicYearSemester.xaml
    /// </summary>
    public partial class Tab_Student_AcademicYearSemester : UserControl
    {
        public Tab_Student_AcademicYearSemester()
        {
            InitializeComponent();
        }

        private void comboBoxYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = comboBoxYear.SelectedIndex;
            Object selectedItem = comboBoxYear.SelectedValue;
            textBox.Text = selectedItem.ToString();

        }
    }
}
