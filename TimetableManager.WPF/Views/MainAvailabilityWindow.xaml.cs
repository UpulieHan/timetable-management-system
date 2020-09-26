using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace TimetableManager.WPF.Views
{
    /// <summary>
    /// Interaction logic for MainAvailabilityWindow.xaml
    /// </summary>
    public partial class MainAvailabilityWindow : Window
    {
        public string val;
        public List<Lecturer> LecturersList { get;  set; }
        public List<GroupId> GroupIdList { get;  set; }
        public List<SubGroupId> SubGroupIdList { get;  set; }
        public List<Session> SessionList { get; set; }
        public ObservableCollection<string> NameList { get;  set; }

        public List<TimeSlot> TimeSlotList { get; set; }
        public ObservableCollection<string> TimeSlotNameList { get; set; }
        public MainAvailabilityWindow()
        {
            InitializeComponent();
            NameList = new ObservableCollection<string>();
            this.DataContext = this;

            TimeSlotNameList = new ObservableCollection<string>();
            LoadTimeSlots();
        }

        private void comboBoxSelectRes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Object selectedItem = comboBoxSelectRes.SelectedValue;
            val = selectedItem.ToString();
            NameList.Clear();
            if (val.Equals("lecturer"))
            {
                _ = this.SetLectures();

            }
            else if (val.Equals("session"))
            {
                _ = this.SetSessions();
            }
            else if (val.Equals("group"))
            {
                _ = this.SetGroupId();

            }
            else
            {
                _ = this.SetSubGroupId();

            }
        }
        private async Task SetGroupId()
        {
            GroupIdDataService groupIdDataService = new GroupIdDataService(new EntityFramework.TimetableManagerDbContext());

            GroupIdList = await groupIdDataService.GetGroupId();

            GroupIdList.ForEach(list =>
            {
                NameList.Add(list.GroupID);

            });
        }
        private async Task SetSubGroupId()
        {
            SubGroupIdDataService subGroupIdDataService = new SubGroupIdDataService(new EntityFramework.TimetableManagerDbContext());

            SubGroupIdList = await subGroupIdDataService.GetSubGroupId();

            SubGroupIdList.ForEach(list =>
            {
                NameList.Add(list.SubGroupID);

            });
        }
        private async Task SetLectures()
        {

            LecturerDataService lecturerDataService = new LecturerDataService(new EntityFramework.TimetableManagerDbContext());

            LecturersList = await lecturerDataService.GetLecturersAsync();

            LecturersList.ForEach(list =>
            {
                NameList.Add(list.EmployeeName);
            });
        }

        private async Task SetSessions()
        {
            SessionDataService sessionDataService = new SessionDataService(new EntityFramework.TimetableManagerDbContext());

            SessionList = await sessionDataService.GetListAsync();

            SessionList.ForEach(e =>
            {
                NameList.Add(e.SessionId.ToString());
            });
        }
      
        private async void LoadTimeSlots()
        {
            TimeSlotDataService timeSlotDataService = new TimeSlotDataService(new EntityFramework.TimetableManagerDbContext());

            TimeSlotList = await timeSlotDataService.GetTimeSlotsAsync();

            TimeSlotList.ForEach(e =>
            {
                TimeSlotNameList.Add(e.CodeId);
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

        private void comboBoxResVal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Object selectedItem = comboBoxSelectRes.SelectedValue;
            string v = selectedItem.ToString();

            if(v.Equals("session"))
            {
                string sid = (string)comboBoxResVal.SelectedItem;

                if(sid != null)
                {
                    Session selected = SessionList.Single(e => e.SessionId == Int32.Parse(sid));

                    SetSessionLabel(selected);
                }
            } else
            {
                CardLecturerName.Content = "";
                CardSubjectName.Content = "";
                CardTagName.Content = "";
                CardGroupName.Content = "";
                CardCount.Content = "";
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            object v = comboBoxSelectRes.SelectedValue;
            string value = v.ToString();
            string timeSlot = (string)comboBoxDay.SelectedItem;
            TimeSlot selectedTimeSlot = TimeSlotList.Single(e => e.CodeId == timeSlot);

            if (value == "lecturer")
            {
                string lecturer = (string)comboBoxResVal.SelectedItem;
                Lecturer selectedLecturer = LecturersList.Single(e => e.EmployeeName == lecturer);

                LecturerDataService lecturerDataService = new LecturerDataService(new EntityFramework.TimetableManagerDbContext());

                _ = lecturerDataService.SetUnAvailable(selectedLecturer, selectedTimeSlot);
            }
        }
    }
}
