using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TimetableManager.Domain.Models;
using TimetableManager.EntityFramework.Services;

namespace TimetableManager.WPF.UserControls.StudentUserControls
{
    /// <summary>
    /// Interaction logic for Tab_Student_AcademicYearSemester_Update.xaml
    /// </summary>
    public partial class Tab_Student_AcademicYearSemester_Update : Window
    {
        public int YSId;
        string Year;
        string Semester;
        string YearShortname;
        string SemesterShortname;
        Year_Semester year_Semester = new Year_Semester();
        public ObservableCollection<Year_Semester> YsDataList { get; private set; }
        public List<Year_Semester> YsList { get; private set; }
        public Tab_Student_AcademicYearSemester_Update(int id)
        {
            InitializeComponent();
            this.DataContext = this;
            YSId = id;
            _ = loadYsData();


        }

        private async Task<bool> loadYsData()
        {
            Year_SemesterDataService year_SemesterData = new Year_SemesterDataService(new EntityFramework.TimetableManagerDbContext());

            Year_Semester yst = await year_SemesterData.GetYsById(this.YSId);

            comboBoxYear.SelectedItem = yst.YsYear;
            comboBoxSemester.SelectedItem = yst.YsSemester;
            textBox.Text = yst.YsShortName;
            return true;
        }
        private void comboBoxYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Object selectedItem = comboBoxYear.SelectedValue;
            Year = selectedItem.ToString();
            if (Year.Equals("Year 1"))
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
            if (Year.Equals("Year 5"))
            {
                YearShortname = "Y5";
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

            textBox.Text = YearShortname + SemesterShortname;

        }
        private async void btnSave_Click_1(object sender, RoutedEventArgs e)
        {
            var year_SemesterDataService = new Year_SemesterDataService(new EntityFramework.TimetableManagerDbContext());
            if (textBox.Text != "")
            {
                year_Semester.YsYear = Year;
                year_Semester.YsSemester = Semester;
                year_Semester.YsShortName = YearShortname + SemesterShortname;
                await year_SemesterDataService.UpdateYs(year_Semester,YSId);
                MessageBox.Show("Updated!");

            }
            else
            {
                MessageBox.Show("fill all fields!!");
            }
        }
    }
}
