using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        private bool isEditState = false;
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
                if(isEditState)
                {
                    year_Semester.YsYear = Year;
                    year_Semester.YsSemester = Semester;
                    year_Semester.YsShortName = YearShortname + "." + SemesterShortname;
                    await year_SemesterDataService.UpdateYs(year_Semester, year_Semester.YsId);
                    isEditState = false;
                } else
                {
                    Year_Semester yearSemester = new Year_Semester
                    {
                        YsYear = Year,
                        YsSemester = Semester,
                        YsShortName = YearShortname + "." + SemesterShortname
                    };

                    await year_SemesterDataService.AddYs(yearSemester);
                }
                
            }
            else
            {
                MessageBox.Show("fill all fields!!");
            }

            YsDataList.Clear();
            _ = this.load();
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Year_Semester ys = (Year_Semester)dataGridYs.SelectedItem;
            _ = LoadYearDataForEdit(ys.YsId);
        }

        private async Task LoadYearDataForEdit(int id)
        {
            Year_SemesterDataService year_SemesterData = new Year_SemesterDataService(new EntityFramework.TimetableManagerDbContext());

            year_Semester = await year_SemesterData.GetYsById(id);

            comboBoxYear.Text = year_Semester.YsYear;
            comboBoxSemester.Text = year_Semester.YsSemester;
            textBox.Text = year_Semester.YsShortName;

            isEditState = true;
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
