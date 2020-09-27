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
    /// Interaction logic for RoomConfigWindow.xaml
    /// </summary>
    public partial class RoomConfigWindow : Window
    {
        private ObservableCollection<string> ValueList { get; set; }
        private ObservableCollection<string> BuildingNameList { get; set; }
        private ObservableCollection<LoadRoomDataGridModel> LoadRoomDataGridList { get; set; }
        private List<Tag> TagList { get; set; }
        private List<Subject> SubjectList { get; set; }
        private List<Lecturer> LecturerList { get; set; }
        private List<Session> SessionList { get; set; }
        private List<GroupId> GroupList { get; set; }
        private List<SubGroupId> SubGroupList { get; set; }
        private List<Building> BuildingList { get; set; }
        private List<Room> RoomList { get; set; }
        private List<Room> SelectedRoomList { get; set; }
        public RoomConfigWindow()
        {
            InitializeComponent();

            ValueList = new ObservableCollection<string>();
            BuildingNameList = new ObservableCollection<string>();
            LoadRoomDataGridList = new ObservableCollection<LoadRoomDataGridModel>();

            ValueListComboBox.ItemsSource = ValueList;
            BuildingComboBox.ItemsSource = BuildingNameList;
            LoadRoomDataGrid.ItemsSource = LoadRoomDataGridList;

            DataContext = this;

            SelectedRoomList = new List<Room>();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selected = (ComboBoxItem)TypeComboBox.SelectedItem;
            string value = selected.Content.ToString();

            if (value == "Tag")
            {
                LoadTagList();
            }
            else if (value == "Subject")
            {
                LoadSubjectList();
            }
            else if (value == "Lecturer")
            {
                LoadLecturerList();
            }
            else if (value == "Session")
            {
                LoadSeesionList();
            }
            else if (value == "Group")
            {
                LoadGroupList();
            }
            else if (value == "Sub Group")
            {
                LoadSubGroupList();
            }
        }

        private async void LoadTagList()
        {
            TagDataService tagDataService = new TagDataService(new EntityFramework.TimetableManagerDbContext());

            TagList = await tagDataService.GetTags();

            ValueList.Clear();
            TagList.ForEach(e =>
            {
                ValueList.Add(e.TagName);
            });
        }

        private async void LoadSubjectList()
        {
            SubjectDataService subjectDataService = new SubjectDataService(new EntityFramework.TimetableManagerDbContext());

            SubjectList = await subjectDataService.GetSubjectsAsync();

            ValueList.Clear();
            SubjectList.ForEach(e =>
            {
                ValueList.Add(e.SubjectName);
            });
        }

        private async void LoadLecturerList()
        {
            LecturerDataService lecturerDataService = new LecturerDataService(new EntityFramework.TimetableManagerDbContext());

            LecturerList = await lecturerDataService.GetLecturersAsync();

            ValueList.Clear();
            LecturerList.ForEach(e =>
            {
                ValueList.Add(e.EmployeeName);
            });
        }

        private async void LoadSeesionList()
        {
            SessionDataService sessionDataService = new SessionDataService(new EntityFramework.TimetableManagerDbContext());

            SessionList = await sessionDataService.GetListAsync();

            ValueList.Clear();
            SessionList.ForEach(e =>
            {
                ValueList.Add(e.SessionId.ToString());
            });
        }

        private async void LoadGroupList()
        {
            GroupIdDataService groupIdDataService = new GroupIdDataService(new EntityFramework.TimetableManagerDbContext());

            GroupList = await groupIdDataService.GetGroupId();

            ValueList.Clear();
            GroupList.ForEach(list =>
            {
                ValueList.Add(list.GroupID);
            });
        }
        private async void LoadSubGroupList()
        {
            SubGroupIdDataService subGroupIdDataService = new SubGroupIdDataService(new EntityFramework.TimetableManagerDbContext());

            SubGroupList = await subGroupIdDataService.GetSubGroupId();

            ValueList.Clear();
            SubGroupList.ForEach(list =>
            {
                ValueList.Add(list.SubGroupID);
            });
        }
        private async void LoadBuildingList()
        {
            BuildingDataService buildingDataService = new BuildingDataService(new EntityFramework.TimetableManagerDbContext());

            BuildingList = await buildingDataService.GetBuildingsAsync();

            BuildingNameList.Clear();
            BuildingList.ForEach(e =>
            {
                BuildingNameList.Add(e.BuildingName);
            });
        }
        private async void LoadRoomList(string BuildingName)
        {
            RoomDataService roomDataService = new RoomDataService(new EntityFramework.TimetableManagerDbContext());

            RoomList = await roomDataService.GetRoomAsync();

            RoomList.RemoveAll(e => e.Building.BuildingName != BuildingName);

            LoadRoomDataGridList.Clear();
            RoomList.ForEach(e =>
            {
                LoadRoomDataGridList.Add(new LoadRoomDataGridModel { Id = e.RoomId, RoomName = e.RoomName });
            });
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

        private void ValueListComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selected = (ComboBoxItem)TypeComboBox.SelectedItem;
            string value = selected.Content.ToString();

            if (value == "Session")
            {
                string sid = (string)ValueListComboBox.SelectedItem;

                if (sid != null)
                {
                    Session selectedSession = SessionList.Single(e => e.SessionId == Int32.Parse(sid));

                    SetSessionLabel(selectedSession);
                }
            }
            else
            {
                CardLecturerName.Content = "";
                CardSubjectName.Content = "";
                CardTagName.Content = "";
                CardGroupName.Content = "";
                CardCount.Content = "";
            }
        }

        private void LoadRoomButton_Click(object sender, RoutedEventArgs e)
        {
            LoadBuildingList();
            RoomConfigTabControl.SelectedIndex = 2;
        }

        private void BuildingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object selected = BuildingComboBox.SelectedItem;
            string value = selected.ToString();
            LoadRoomList(value);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            LoadRoomDataGridModel room = (LoadRoomDataGridModel)LoadRoomDataGrid.SelectedItem;

            SelectedRoomList.Add(RoomList.Find(e => e.RoomId == room.Id));
            SetRoomTextBox();
        }

        private void SetRoomTextBox()
        {
            string s = "";

            SelectedRoomList.ForEach(e =>
            {
                s = s + e.RoomName + ", ";
            });

            RoomTextBox.Text = s;
        }
    }

    public class LoadRoomDataGridModel {
        public int Id { get; set; }
        public string RoomName { get; set; }
    }
}
