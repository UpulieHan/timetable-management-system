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

namespace TimetableManager.WPF.Controls
{
    /// <summary>
    /// Interaction logic for Tab_Main_Subjects.xaml
    /// </summary>
    public partial class Tab_Main_Subjects : UserControl
    {
        public ObservableCollection<string> YearSemesterNameList { get; private set; }
        public List<Year_Semester> YearSemesterList { get; private set; }

        public ObservableCollection<Subject> SubjectDataList { get; private set; }
        public List<Subject> SubjectList { get; private set; }
        public Tab_Main_Subjects()
        {
            InitializeComponent();

            YearSemesterNameList = new ObservableCollection<string>();
            _ = SetYearSemesterList();

            SubjectDataList = new ObservableCollection<Subject>();
            _ = LoadSubjectDataList();

            this.DataContext = this;
        }

        private async Task SetYearSemesterList()
        {
            Year_SemesterDataService yearSemesterDataService = new Year_SemesterDataService(new EntityFramework.TimetableManagerDbContext());

            YearSemesterList = await yearSemesterDataService.GetYs();

            YearSemesterList.ForEach(list =>
            {
                YearSemesterNameList.Add(list.YsShortName);
            });
        }

        private async Task LoadSubjectDataList()
        {
            SubjectDataService subjectDataService = new SubjectDataService(new EntityFramework.TimetableManagerDbContext());
            SubjectList = await subjectDataService.GetSubjectsAsync();

            SubjectList.ForEach(e =>
            {
                SubjectDataList.Add(e);
            });
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectNameTextBox.Text.Trim() == "" || SubjectCodeTextBox.Text.Trim() == "" || LectureHoursTextBox.Text.Trim() == "" || TutorialHoursTextBox.Text.Trim() == "" || LabHoursTextBox.Text.Trim() == "" || EvaluationHoursTextBox.Text.Trim() == "" || YearSemesterComboBox.SelectedItem == null) 
            {
                MessageBox.Show("Sorry! Fields cannot be empty!", "Error");
                return;
            }

            Subject subject = new Subject
            {
                SubjectName = SubjectNameTextBox.Text.Trim(),
                SubjectCode = SubjectCodeTextBox.Text.Trim(),
                LectureHours = Int32.Parse(LectureHoursTextBox.Text.Trim()),
                TutorialHours = Int32.Parse(TutorialHoursTextBox.Text.Trim()),
                LabHours = Int32.Parse(LabHoursTextBox.Text.Trim()),
                EvaluationHours = Int32.Parse(EvaluationHoursTextBox.Text.Trim()),
                OfferedYearSemester = YearSemesterComboBox.SelectedItem.ToString()
            };

            SubjectDataService subjectDataService = new SubjectDataService(new EntityFramework.TimetableManagerDbContext());

            if(SubjectCodeTextBox.IsEnabled)
            {
                subjectDataService.AddSubject(subject).ContinueWith(result =>
                {
                    if (result != null)
                    {
                        MessageBox.Show("Subject Added!", "Success");
                    }
                    else
                    {
                        MessageBox.Show("Error occured!", "Error");
                    }
                });
            } else
            {
                SubjectCodeTextBox.IsEnabled = true;
                subjectDataService.UpdateSubject(subject).ContinueWith(result =>
                {
                    if (result != null)
                    {
                        MessageBox.Show("Subject Updated!", "Success");
                    }
                    else
                    {
                        MessageBox.Show("Error occured!", "Error");
                    }
                });
            }


            SubjectNameTextBox.Clear();
            SubjectCodeTextBox.Clear();
            LectureHoursTextBox.Clear();
            TutorialHoursTextBox.Clear();
            LabHoursTextBox.Clear();
            EvaluationHoursTextBox.Clear();
            YearSemesterComboBox.SelectedIndex = -1;

            SubjectDataList.Clear();
            _ = LoadSubjectDataList();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            SubjectTabControl.SelectedIndex = 1;
            Subject selectedSubject = (Subject)SubjectDataGrid.SelectedItem;
            LoadSubjectDataForEdit(selectedSubject);
        }

        private void LoadSubjectDataForEdit(Subject subject)
        {
            SubjectNameTextBox.Text = subject.SubjectName;
            SubjectCodeTextBox.Text = subject.SubjectCode;
            SubjectCodeTextBox.IsEnabled = false;
            LectureHoursTextBox.Text = subject.LectureHours.ToString();
            TutorialHoursTextBox.Text = subject.TutorialHours.ToString();
            LabHoursTextBox.Text =subject.LabHours.ToString();
            EvaluationHoursTextBox.Text = subject.EvaluationHours.ToString();
            YearSemesterComboBox.SelectedItem = subject.OfferedYearSemester;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Subject selectedSubject = (Subject)SubjectDataGrid.SelectedItem;

            SubjectDataService subjectDataService = new SubjectDataService(new EntityFramework.TimetableManagerDbContext());

            subjectDataService.DeleteSubject(selectedSubject.Id).ContinueWith(result =>
            {
                if(result == null)
                {
                    MessageBox.Show("Unable to Delete!", "Error");
                }
            });

            _ = SubjectDataList.Remove(selectedSubject);
        }

        private void CheckTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
