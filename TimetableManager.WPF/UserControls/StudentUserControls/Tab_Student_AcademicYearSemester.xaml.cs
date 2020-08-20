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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimetableManager.Domain.Models;
using TimetableManager.EntityFramework.Services;

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
        Year_Semester year_Semester = new Year_Semester();
        public ObservableCollection<Year_Semester> YsDataList { get; private set; }
        public List<Year_Semester> YsList { get; private set; }
        public Tab_Student_AcademicYearSemester()
        {
            InitializeComponent();
            this.DataContext = this;
            YsDataList = new ObservableCollection<Year_Semester>();
            _ = this.load();

        }
        public async Task load()
        {
            Year_SemesterDataService year_SemesterDataService = new Year_SemesterDataService(new EntityFramework.TimetableManagerDbContext());
            YsList = await year_SemesterDataService.GetYs();
            YsList.ForEach(e =>
            {
                Year_Semester l = new Year_Semester();
                l.YsId = e.YsId;
                l.YsYear = e.YsYear;
                l.YsSemester = e.YsSemester;
                l.YsShortName = e.YsShortName;
                YsDataList.Add(l);
            });

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
        private async void  btnSave_Click_1(object sender, RoutedEventArgs e)
        {
            var year_SemesterDataService = new Year_SemesterDataService(new EntityFramework.TimetableManagerDbContext());
            if (textBox.Text != "")
            {
                year_Semester.YsYear = Year;
                year_Semester.YsSemester = Semester;
                year_Semester.YsShortName = YearShortname + SemesterShortname;
                await year_SemesterDataService.AddYs(year_Semester);
            }
            else
            {
                MessageBox.Show("fill all fields!!");
            }
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Edit button clicked");
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Year_Semester ys = (Year_Semester)dataGridYs.SelectedItem;

            Year_SemesterDataService year_SemesterDataService = new Year_SemesterDataService(new EntityFramework.TimetableManagerDbContext());

            year_SemesterDataService.DeleteYear_Semester(ys.YsId).ContinueWith(result =>
            {
                MessageBox.Show("Deleted");
            });

            _ = YsDataList.Remove(ys);
        }
    }
}
