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
        List<SubGroupId> SubGroupIdsList { get; set; }
        List<Subject> SubjectsList { get; set; }

        List<Lecturer> SelectedLecturerList { get; set; }
        List<Tag> SelectedTagList { get; set; }
        List<GroupId> SelectedGroupIdList { get; set; }
        List<SubGroupId> SelectedSubGroupIdList { get; set; }
        List<Subject> SelectedSubjectList { get; set; }

        public List<Session> SessionList { get; set; }
        public ObservableCollection<SessionDataGridModel> SessionDataGridList { get; set; }
        public SessionsWindow()
        {
            InitializeComponent();
            DataCollection = new ObservableCollection<LoadDataGridModel>();

            SelectedLecturerList = new List<Lecturer>();
            SelectedTagList = new List<Tag>();
            SelectedGroupIdList = new List<GroupId>();
            SelectedSubGroupIdList = new List<SubGroupId>();
            SelectedSubjectList = new List<Subject>();

            DataGrid.ItemsSource = DataCollection;

            SessionDataGridList = new ObservableCollection<SessionDataGridModel>();
            LoadSessionData();
            SessionDataGrid.ItemsSource = SessionDataGridList;
        }

        private async void LoadSessionData()
        {
            SessionDataService sessionDataService = new SessionDataService(new EntityFramework.TimetableManagerDbContext());

            SessionList = await sessionDataService.GetListAsync();

            SessionList.ForEach(e =>
            {
                SessionDataGridList.Add(new SessionDataGridModel
                {
                    SessionId = e.SessionId,
                    Subject = e.Subject.SubjectName
                });
            });
        }

        private void ChangeToLoadTab()
        {
            SessionTabControl.SelectedIndex = 2;
        }
        private void ChangeToSessionTab()
        {
            SessionTabControl.SelectedIndex = 1;
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
            ComboBoxItem item = (ComboBoxItem)GroupComboBox.SelectedItem;
            if(item.Content.ToString() == "Groups")
            {
                LoadType = "GroupId";
                LoadGroupIdData();
            } 
            else if(item.Content.ToString() == "Sub Groups")
            {
                LoadType = "SubGroups";
                LoadSubGroupIdData();
            }

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

        private async void LoadSubGroupIdData()
        {
            SubGroupIdDataService subGroupIdDataService = new SubGroupIdDataService(new EntityFramework.TimetableManagerDbContext());

            SubGroupIdsList = await subGroupIdDataService.GetSubGroupId();

            DataCollection.Clear();
            SubGroupIdsList.ForEach(e =>
            {
                DataCollection.Add(new LoadDataGridModel { Id = e.Id, ItemName = e.SubGroupID });
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
                ChangeToSessionTab();
            }
            else if(LoadType == "Tag")
            {
                SelectedTagList.Add(TagsList.Find(e => e.TagId == item.Id));
                SetTagsTextBox();
                ChangeToSessionTab();

            }
            else if(LoadType == "GroupId")
            {
                SelectedGroupIdList.Add(GroupIdsList.Find(e => e.Id == item.Id));
                SetGroupIdTextBox();
                ChangeToSessionTab();

            }
            else if(LoadType == "SubGroups")
            {
                SelectedSubGroupIdList.Add(SubGroupIdsList.Find(e => e.Id == item.Id));
                SetSubGroupIdTextBox();
                ChangeToSessionTab();

            }
            else if(LoadType == "Subject")
            {
                SelectedSubjectList.Add(SubjectsList.Find(e => e.Id == item.Id));
                SetSubjectsTextBox();
                ChangeToSessionTab();

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
        private void SetSubGroupIdTextBox()
        {
            string s = "";

            SelectedSubGroupIdList.ForEach(e =>
            {
                s = s + e.SubGroupID + " ,";
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

            int studentCount = Int32.Parse(StudentsNumberTextBox.Text.Trim());
            int duration = Int32.Parse(DurationTextBox.Text.Trim());

            Session session = new Session
            {
                StudentCount = studentCount,
                Duration = duration
            };

            sessionDataService.AddSession(session, SelectedLecturerList, SelectedGroupIdList, SelectedSubGroupIdList, SelectedTagList[0], SelectedSubjectList[0]).ContinueWith(result =>
            {
                if (result != null)
                {
                    MessageBox.Show("Session Added Successfully!", "Success");
                }
                else
                {
                    MessageBox.Show("Sorry! Error occured!", "Error");
                }
            });
            clear();
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            SessionDataGridModel item = (SessionDataGridModel)SessionDataGrid.SelectedItem;

            Session selectedSession = SessionList.Single(e => e.SessionId == item.SessionId);

            SetSessionLabel(selectedSession);
        }

        private void SetSessionLabel(Session session)
        {
            var lNames = "";
            session.LecturerSessions.ForEach(e =>
            {
                lNames += e.Lecturer.EmployeeName + " ,";
            });

            var gNames = "";
            if (session.GroupIdSessions.Count != 0)
            {
                session.GroupIdSessions.ForEach(e =>
                {
                    gNames += e.Group.GroupID + " ,";
                });
            }
            else if (session.SubGroupIdSessions.Count != 0)
            {
                session.SubGroupIdSessions.ForEach(e =>
                {
                    gNames += e.SubGroup.SubGroupID + " ,";
                });
            }

            CardLecturerName.Content = lNames;
            CardSubjectName.Content = session.Subject.SubjectName;
            CardTagName.Content = session.Tag.TagName;
            CardGroupName.Content = gNames; 
            CardCount.Content = session.StudentCount + "(" + session.Duration + ")";

        }
        private void clear()
        {
            LecturersTextBox.Text = "";
            TagsTextBox.Text = "";
            GroupsTextBox.Text = "";
            SubjectsTextBox.Text = "";
            StudentsNumberTextBox.Text = "";
            DurationTextBox.Text = "";
    }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Sorry! This feature is under development!",  "Coming Soon");
        }
    }

    public class LoadDataGridModel
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
    }

    public class SessionDataGridModel
    {
        public int SessionId { get; set; }
        public string Subject { get; set; }
    }
   
}
