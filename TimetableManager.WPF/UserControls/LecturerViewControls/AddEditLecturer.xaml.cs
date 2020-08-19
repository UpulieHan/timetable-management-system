using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Windows.Threading;
using TimetableManager.Domain.Models;
using TimetableManager.EntityFramework.Services;

namespace TimetableManager.WPF.UserControls.LecturerViewControls
{
    /// <summary>
    /// Interaction logic for AddEditLecturer.xaml
    /// </summary>
    public partial class AddEditLecturer : UserControl
    {
        public ObservableCollection<string> FacultyNameList { get; private set; }
        public ObservableCollection<string> DepartmentNameList { get; private set; }
        public ObservableCollection<string> CenterNameList { get; private set; }
        public ObservableCollection<string> BuildingNameList { get; private set; }
        public ObservableCollection<string> LevelNameList { get; private set; }
        public List<Faculty> FacultyList { get; private set; }
        public List<Center> CenterList { get; private set; }
        public List<Level> LevelList { get; private set; }
        public AddEditLecturer()
        {
            InitializeComponent();
            FacultyNameList = new ObservableCollection<string>();
            DepartmentNameList = new ObservableCollection<string>();
            CenterNameList = new ObservableCollection<string>();
            BuildingNameList = new ObservableCollection<string>();
            LevelNameList = new ObservableCollection<string>();

            this.DataContext = this;

            this.Dispatcher.Invoke(
                DispatcherPriority.ApplicationIdle,
                new Action(() =>
                {
                    _ = this.SetFacultyList();
                    _ = this.SetCenterList();
                    _ = this.SetLevelList();
                }));
        }

        private async Task SetFacultyList()
        {
            FacultyDataService facultyDataService = new FacultyDataService(new EntityFramework.TimetableManagerDbContext());

            FacultyList = await facultyDataService.GetFacultiesAsync();

            FacultyList.ForEach(list =>
            {
                FacultyNameList.Add(list.FacultyName);
            });
        }
        private async Task SetCenterList()
        {
            CenterDataService centerDataService = new CenterDataService(new EntityFramework.TimetableManagerDbContext());

            CenterList = await centerDataService.GetCentersAsync();

            CenterList.ForEach(e =>
            {
                CenterNameList.Add(e.CenterName);
            });
        }        
        private async Task SetLevelList()
        {
            LevelDataService levelDataService = new LevelDataService(new EntityFramework.TimetableManagerDbContext());

            LevelList = await levelDataService.GetLevelsAsync();

            LevelList.ForEach(e =>
            {
                LevelNameList.Add(e.LevelName);
            });
        }

        private void FacutlyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String faculty = FacutlyComboBox.SelectedItem.ToString();

            FacultyList.ForEach(e =>
            {
                if(e.FacultyName.Equals(faculty))
                {
                    DepartmentNameList.Clear();
                    e.Departments.ForEach(d =>
                    {
                        DepartmentNameList.Add(d.DepartmentName);
                    });
                }
            });
        }

        private void CenterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string center = CenterComboBox.SelectedItem.ToString();

            CenterList.ForEach(e =>
            {
                if(e.CenterName.Equals(center))
                {
                    BuildingNameList.Clear();
                    e.Buildings.ForEach(b =>
                    {
                        BuildingNameList.Add(b.BuildingName);
                    });
                }
            });
        }

        private void LevelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string LevelName = LevelComboBox.SelectedItem.ToString();
            string EmployeeId = EmployeeIdTextBox.Text.Trim();

            LevelList.ForEach(e =>
            {
                if(e.LevelName.Equals(LevelName))
                {
                    RankTextBox.Clear();
                    RankTextBox.Text = e.LevelId.ToString() + "." + EmployeeId;
                }
            });
        }

        // This prevents user from inputing letters to the employee id
        private void EmployeeIdTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

            // Change the rank when editing Employee ID only if a rank selected
            if(LevelComboBox.SelectedItem != null)
            {
                string LevelName = LevelComboBox.SelectedItem.ToString();
                string EmployeeId = EmployeeIdTextBox.Text.Trim();

                LevelList.ForEach(e =>
                {
                    if (e.LevelName.Equals(LevelName))
                    {
                        RankTextBox.Clear();
                        RankTextBox.Text = e.LevelId.ToString() + "." + EmployeeId;
                    }
                });
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Lecturer lecturer = new Lecturer();
            lecturer.EmployeeId = Int32.Parse(EmployeeIdTextBox.Text.Trim());
            lecturer.EmployeeName = EmployeeNameTextBox.Text.Trim();
            lecturer.Rank = float.Parse(RankTextBox.Text.Trim());

            string facultyName = FacutlyComboBox.SelectedItem.ToString();
            string departmentName = DepartmentComboBox.SelectedItem.ToString();
            string centerName = CenterComboBox.SelectedItem.ToString();
            string buildingName = BuildingComboBox.SelectedItem.ToString();
            string levelName = LevelComboBox.SelectedItem.ToString();

            LecturerDataService lecturerDataService = new LecturerDataService(new EntityFramework.TimetableManagerDbContext());

            lecturerDataService.AddLecturer(lecturer, facultyName, departmentName, centerName, buildingName, levelName).ContinueWith(result =>
            {
                if(result != null)
                {
                    MessageBox.Show("Lecture Added!", "Success");
                } else
                {
                    MessageBox.Show("Sorry! Error occured!", "Error");
                }
            });
        }
    }
}
