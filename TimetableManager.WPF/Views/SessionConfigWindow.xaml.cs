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
    /// Interaction logic for SessionConfigWindow.xaml
    /// </summary>
    public partial class SessionConfigWindow : Window
    {
        private List<Session> SessionList { get; set; }
        private List<Session> SelectedSessionList { get; set; }
        private ObservableCollection<string> SessionNameList { get; set; }
        public SessionConfigWindow()
        {
            InitializeComponent();

            SessionNameList = new ObservableCollection<string>();

            LoadSeesionList();

            SessionOneComboBox.ItemsSource = SessionNameList;
            SessionTwoComboBox.ItemsSource = SessionNameList;
        }

        private async void LoadSeesionList()
        {
            SessionDataService sessionDataService = new SessionDataService(new EntityFramework.TimetableManagerDbContext());

            SessionList = await sessionDataService.GetListAsync();

            SessionNameList.Clear();
            SessionList.ForEach(e =>
            {
                SessionNameList.Add(e.SessionId.ToString());
            });
        }

        private void SessionOneComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string oneId = (string)SessionOneComboBox.SelectedItem;

            if (oneId != null)
            {
                Session selectedSession = SessionList.Single(e => e.SessionId == Int32.Parse(oneId));

                SetSessionLabel(selectedSession, 1);
            }
        }
        private void SessionTwoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string twoId = (string)SessionTwoComboBox.SelectedItem;

            if (twoId != null)
            {
                Session selectedSession = SessionList.Single(e => e.SessionId == Int32.Parse(twoId));

                SetSessionLabel(selectedSession, 2);
            }
        }

        private void SetSessionLabel(Session session, int index)
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

            if(index == 1)
            {
                CardLecturerName1.Content = lNames;
                CardSubjectName1.Content = session.Subject.SubjectName;
                CardTagName1.Content = session.Tag.TagName;
                CardGroupName1.Content = gNames;
                CardCount1.Content = session.StudentCount + "(" + session.Duration + ")";
            }
            else if(index == 2)
            {
                CardLecturerName2.Content = lNames;
                CardSubjectName2.Content = session.Subject.SubjectName;
                CardTagName2.Content = session.Tag.TagName;
                CardGroupName2.Content = gNames;
                CardCount2.Content = session.StudentCount + "(" + session.Duration + ")";
            }

        }
        private void ClearCard1()
        {
            CardLecturerName1.Content = "";
            CardSubjectName1.Content = "";
            CardTagName1.Content = "";
            CardGroupName1.Content = "";
            CardCount1.Content = "";
        }
        private void ClearCard2()
        {
            CardLecturerName2.Content = "";
            CardSubjectName2.Content = "";
            CardTagName2.Content = "";
            CardGroupName2.Content = "";
            CardCount2.Content = "";
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _ = SetConsecutive().ContinueWith(result =>
            {
                if (result != null)
                {
                    MessageBox.Show("Consecutive session Added!", "Success");
                }
                else
                {
                    MessageBox.Show("Sorry! Error occured!", "Error");
                }
            });
            ClearCard1();
            ClearCard2();
        }

        private void ParallelSaveButton_Click(object sender, RoutedEventArgs e)
        {
            _ = SetConsecutive().ContinueWith(result =>
            {
                if (result != null)
                {
                    MessageBox.Show("Parallel session Added!", "Success");
                }
                else
                {
                    MessageBox.Show("Sorry! Error occured!", "Error");
                }
            });
            ClearCard1();
            ClearCard2();
        }

        private async Task<int> SetConsecutive()
        {
            string oneId = (string)SessionOneComboBox.SelectedItem;
            string twoId = (string)SessionTwoComboBox.SelectedItem;

            Session one = SessionList.Single(e => e.SessionId == Int32.Parse(oneId));
            Session two = SessionList.Single(e => e.SessionId == Int32.Parse(twoId));

            SessionDataService sessionDataService = new SessionDataService(new EntityFramework.TimetableManagerDbContext());

            return await  sessionDataService.SetConsecutiveSessions(one, two);
        }
    }
}
