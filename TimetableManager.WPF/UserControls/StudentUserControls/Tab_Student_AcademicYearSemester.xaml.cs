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
        string Year;
        string Semester;
        string YearShortname;
        string SemesterShortname;
        public Tab_Student_AcademicYearSemester()
        {
            InitializeComponent();
        }

        private void comboBoxYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Object selectedItem = comboBoxYear.SelectedValue;
            Year = selectedItem.ToString();
            if(Year.Equals("Year 1"))
            {
                YearShortname = "Y1";
            }
            if (Year.Equals("Year 2"))
            {
                YearShortname = "Y2";
            }
            if (Year.Equals("Year 3"))
            {
                YearShortname = "Y3";
            }
            if (Year.Equals("Year 4"))
            {
                YearShortname = "Y4";
            }
            textBox.Text = YearShortname + SemesterShortname;


        }

        private void comboBoxSemester_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Object selectedItem = comboBoxSemester.SelectedValue;
            Semester = selectedItem.ToString();
            if (Semester.Equals("Semester 1"))
            {
                SemesterShortname = "S1";
            }
            if (Semester.Equals("Semester 2"))
            {
                SemesterShortname = "S2";
            }
            if (Semester.Equals("Semester 3"))
            {
                SemesterShortname = "S3";
            }
            if (Semester.Equals("Semester 4"))
            {
                SemesterShortname = "S4";
            }
            textBox.Text = YearShortname + SemesterShortname;

        }

    }
}
