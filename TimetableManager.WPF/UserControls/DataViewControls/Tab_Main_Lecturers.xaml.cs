using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TimetableManager.Domain.Models;
using TimetableManager.EntityFramework.Services;
using TimetableManager.WPF.UserControls.LecturerViewControls;

namespace TimetableManager.WPF.Controls
{
    /// <summary>
    /// Interaction logic for Tab_Main_Lecturers.xaml
    /// </summary>
    public partial class Tab_Main_Lecturers : UserControl
    {
        public ObservableCollection<LecturerGridModel> LecturerDataList { get; private set; }
        public List<Lecturer> LecturerList { get; private set; }

        //
        public ObservableCollection<string> FacultyNameList { get; private set; }
        public ObservableCollection<string> DepartmentNameList { get; private set; }
        public ObservableCollection<string> CenterNameList { get; private set; }
        public ObservableCollection<string> BuildingNameList { get; private set; }
        public ObservableCollection<string> LevelNameList { get; private set; }
        public List<Faculty> FacultyList { get; private set; }
        public List<Center> CenterList { get; private set; }
        public List<Level> LevelList { get; private set; }
        public Tab_Main_Lecturers()
        {
            InitializeComponent();

            LecturerDataList = new ObservableCollection<LecturerGridModel>();
            _ = this.LoadLecturerData();

            FacultyNameList = new ObservableCollection<string>();
            DepartmentNameList = new ObservableCollection<string>();
            CenterNameList = new ObservableCollection<string>();
            BuildingNameList = new ObservableCollection<string>();
            LevelNameList = new ObservableCollection<string>();
            _ = this.SetFacultyList();
            _ = this.SetCenterList();
            _ = this.SetLevelList();

            this.DataContext = this;
        }

        private async Task LoadLecturerData()
        {
            LecturerDataService lecturerDataService = new LecturerDataService(new EntityFramework.TimetableManagerDbContext());

            LecturerList = await lecturerDataService.GetLecturersAsync();

            LecturerList.ForEach(e =>
            {
                LecturerGridModel lecturerObj = new LecturerGridModel
                {
                    EmployeeId = e.EmployeeId,
                    EmployeeName = e.EmployeeName,
                    FacultyName = e.Faculty.FacultyName,
                    DepartmentName = e.Department.DepartmentName,
                    CenterName = e.Center.CenterName,
                    BuildingName = e.Building.BuildingName,
                    LevelName = e.Level.LevelName,
                    Rank = e.Rank
                };

                LecturerDataList.Add(lecturerObj);
            });
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            LecturerGridModel lecturer = (LecturerGridModel)LecturerDataGrid.SelectedItem;

            _ = this.LoadDataForEditAsync(lecturer.EmployeeId);
            LecturerTabControl.SelectedIndex = 1;
        }

        private async Task LoadDataForEditAsync(int id)
        {
            LecturerDataService lecturerDataService = new LecturerDataService(new EntityFramework.TimetableManagerDbContext());

            Lecturer lecturer = await lecturerDataService.GetLectureById(id);

            EmployeeNameTextBox.Text = lecturer.EmployeeName;
            EmployeeIdTextBox.Text = lecturer.EmployeeId.ToString();
            EmployeeIdTextBox.IsEnabled = false;
            FacutlyComboBox.SelectedItem = lecturer.Faculty.FacultyName;
            DepartmentComboBox.SelectedItem = lecturer.Department.DepartmentName;
            CenterComboBox.SelectedItem = lecturer.Center.CenterName;
            BuildingComboBox.SelectedItem = lecturer.Building.BuildingName;
            LevelComboBox.SelectedItem = lecturer.Level.LevelName;
            RankTextBox.Text = lecturer.Rank.ToString();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            LecturerGridModel lecturer = (LecturerGridModel)LecturerDataGrid.SelectedItem;

            LecturerDataService lecturerDataService = new LecturerDataService(new EntityFramework.TimetableManagerDbContext());

            lecturerDataService.DeleteLecturer(lecturer.EmployeeId).ContinueWith(result =>
            {
                if (result == null)
                {
                    MessageBox.Show("Sorry! Error occured", "Error");
                }
            });

            _ = LecturerDataList.Remove(lecturer);
        }

        private void FacutlyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FacutlyComboBox.SelectedItem == null)
            {
                return;
            }
            String faculty = FacutlyComboBox.SelectedItem.ToString();

            FacultyList.ForEach(e =>
            {
                if (e.FacultyName.Equals(faculty))
                {
                    DepartmentNameList.Clear();
                    e.Departments.ForEach(d =>
                    {
                        DepartmentNameList.Add(d.DepartmentName);
                    });
                }
            });
        }

        private void EmployeeIdTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

            // Change the rank when editing Employee ID only if a rank selected
            if (LevelComboBox.SelectedItem != null)
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

        private void CenterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CenterComboBox.SelectedItem == null)
            {
                return;
            }
            string center = CenterComboBox.SelectedItem.ToString();

            CenterList.ForEach(e =>
            {
                if (e.CenterName.Equals(center))
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
            if (LevelComboBox.SelectedItem == null)
            {
                return;
            }
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeIdTextBox.Text.Trim() == "" || EmployeeNameTextBox.Text.Trim() == "" || RankTextBox.Text.Trim() == "" || FacutlyComboBox.SelectedItem == null || DepartmentComboBox.SelectedItem == null || CenterComboBox.SelectedItem == null || BuildingComboBox.SelectedItem == null || LevelComboBox.SelectedItem == null)
            {
                MessageBox.Show("Sorry! Fields cannot be empty!", "Error");
                return;
            }
            Lecturer lecturer = new Lecturer
            {
                EmployeeId = Int32.Parse(EmployeeIdTextBox.Text.Trim()),
                EmployeeName = EmployeeNameTextBox.Text.Trim(),
                Rank = float.Parse(RankTextBox.Text.Trim())
            };

            string facultyName = FacutlyComboBox.SelectedItem.ToString();
            string departmentName = DepartmentComboBox.SelectedItem.ToString();
            string centerName = CenterComboBox.SelectedItem.ToString();
            string buildingName = BuildingComboBox.SelectedItem.ToString();
            string levelName = LevelComboBox.SelectedItem.ToString();

            LecturerDataService lecturerDataService = new LecturerDataService(new EntityFramework.TimetableManagerDbContext());

            if (!EmployeeIdTextBox.IsEnabled)
            {
                EmployeeIdTextBox.IsEnabled = true;

                lecturerDataService.UpdateLecturer(lecturer, facultyName, departmentName, centerName, buildingName, levelName).ContinueWith(result =>
                {
                    if (result != null)
                    {
                        MessageBox.Show("Lecture Updated!", "Success");
                    }
                    else
                    {
                        MessageBox.Show("Sorry! Error occured!", "Error");
                    }
                });
            }
            else
            {
                lecturerDataService.AddLecturer(lecturer, facultyName, departmentName, centerName, buildingName, levelName).ContinueWith(result =>
                {
                    if (result != null)
                    {
                        MessageBox.Show("Lecture Added!", "Success");
                    }
                    else
                    {
                        MessageBox.Show("Sorry! Error occured!", "Error");
                    }
                });
            }

            EmployeeIdTextBox.Clear();
            EmployeeNameTextBox.Clear();
            RankTextBox.Clear();
            FacutlyComboBox.SelectedIndex = -1;
            DepartmentComboBox.SelectedIndex = -1;
            CenterComboBox.SelectedIndex = -1;
            BuildingComboBox.SelectedIndex = -1;
            LevelComboBox.SelectedIndex = -1;

            LecturerDataList.Clear();
            _ = this.LoadLecturerData();
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
    }
}
