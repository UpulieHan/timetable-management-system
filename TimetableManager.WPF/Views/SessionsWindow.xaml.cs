using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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

namespace TimetableManager.WPF.Views
{
    /// <summary>
    /// Interaction logic for SessionsWindow.xaml
    /// </summary>
    public partial class SessionsWindow : Window
    {
        private string LoadType { get; set; }
        public ObservableCollection<LoadDataGridModel> DataCollection { get; set; }
        List<Lecturer> LecturersList { get; set; }
        List<Tag> TagsList { get; set; }
        List<GroupId> GroupIdsList { get; set; }
        List<Subject> SubjectsList { get; set; }

        List<Lecturer> SelectedLecturerList { get; set; }
        List<Tag> SelectedTagList { get; set; }
        List<GroupId> SelectedGroupIdList { get; set; }
        List<Subject> SelectedSubjectList { get; set; }

        public SessionsWindow()
        {
            InitializeComponent();
            DataCollection = new ObservableCollection<LoadDataGridModel>();

            SelectedLecturerList = new List<Lecturer>();
            SelectedTagList = new List<Tag>();
            SelectedGroupIdList = new List<GroupId>();
            SelectedSubjectList = new List<Subject>();

            DataGrid.ItemsSource = DataCollection;
        }

        private void ChangeToLoadTab()
        {
            SessionTabControl.SelectedIndex = 2;
        }
        private void LecturerLoadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadType = "Lecturer";
            LoadLecturersData();
            ChangeToLoadTab();
        }

        private void SubjectLoadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadType = "Subject";
            LoadSubjectData();
            ChangeToLoadTab();
        }

        private void TagLoadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadType = "Tag";
            LoadTagsData();
            ChangeToLoadTab();
        }

        private void GroupLoadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadType = "GroupId";
            LoadGroupIdData();
            ChangeToLoadTab();
        }

        private async void LoadLecturersData()
        {
            LecturerDataService lecturerDataService = new LecturerDataService(new EntityFramework.TimetableManagerDbContext());

            LecturersList = await lecturerDataService.GetLecturersAsync();

            DataCollection.Clear();
            LecturersList.ForEach(e =>
            {
                DataCollection.Add(new LoadDataGridModel { Id = e.EmployeeId, ItemName = e.EmployeeName });
            });
        }

        private async void LoadTagsData()
        {
            TagDataService tagDataService = new TagDataService(new EntityFramework.TimetableManagerDbContext());

            TagsList = await tagDataService.GetTags();

            DataCollection.Clear();
            TagsList.ForEach(e =>
            {
                DataCollection.Add(new LoadDataGridModel { Id = e.TagId, ItemName = e.TagName });
            });
        }

        private async void LoadGroupIdData()
        {
            GroupIdDataService groupIdDataService = new GroupIdDataService(new EntityFramework.TimetableManagerDbContext());

            GroupIdsList = await groupIdDataService.GetGroupId();

            DataCollection.Clear();
            GroupIdsList.ForEach(e =>
            {
                DataCollection.Add(new LoadDataGridModel { Id = e.Id, ItemName = e.GroupID });
            });
        }

        private async void LoadSubjectData()
        {
            SubjectDataService subjectDataService = new SubjectDataService(new EntityFramework.TimetableManagerDbContext());

            SubjectsList = await subjectDataService.GetSubjectsAsync();

            DataCollection.Clear();
            SubjectsList.ForEach(s =>
            {
                DataCollection.Add(new LoadDataGridModel { Id = s.Id, ItemName = s.SubjectName });
            });
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            LoadDataGridModel item = (LoadDataGridModel)DataGrid.SelectedItem;
            if (LoadType == "Lecturer")
            {
                SelectedLecturerList.Add(LecturersList.Find(e => e.EmployeeId == item.Id));
                SetLecturerTextBox();
            }
            else if(LoadType == "Tag")
            {
                SelectedTagList.Add(TagsList.Find(e => e.TagId == item.Id));
                SetTagsTextBox();
            }
            else if(LoadType == "GroupId")
            {
                SelectedGroupIdList.Add(GroupIdsList.Find(e => e.Id == item.Id));
                SetGroupIdTextBox();
            } 
            else if(LoadType == "Subject")
            {
                SelectedSubjectList.Add(SubjectsList.Find(e => e.Id == item.Id));
                SetSubjectsTextBox();
            }
        }

        private void SetLecturerTextBox()
        {
            string s = "";

            SelectedLecturerList.ForEach(e =>
            {
                s = s + e.EmployeeName + " ,";
            });

            LecturersTextBox.Text = s;
        }

        private void SetTagsTextBox()
        {
            string s = "";

            SelectedTagList.ForEach(e =>
            {
                s = s + e.TagName + " ,";
            });

            TagsTextBox.Text = s;
        }

        private void SetGroupIdTextBox()
        {
            string s = "";

            SelectedGroupIdList.ForEach(e =>
            {
                s = s + e.GroupID + " ,";
            });

            GroupsTextBox.Text = s;
        }        
        private void SetSubjectsTextBox()
        {
            string s = "";

            SelectedSubjectList.ForEach(e =>
            {
                s = s + e.SubjectName + " ,";
            });

            SubjectsTextBox.Text = s;
        }

        private void AddSessionButton_Click(object sender, RoutedEventArgs e)
        {
            SessionDataService sessionDataService = new SessionDataService(new EntityFramework.TimetableManagerDbContext());


        }
    }

    public class LoadDataGridModel
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
    }
}
