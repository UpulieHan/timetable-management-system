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

        public ObservableCollection<string> NameList { get;  set; }


        public MainAvailabilityWindow()
        {
            InitializeComponent();
            NameList = new ObservableCollection<string>();
            this.DataContext = this;

        }

        private void comboBoxSelectRes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Object selectedItem = comboBoxSelectRes.SelectedValue;
            val = selectedItem.ToString();
            if (val.Equals("lecturer"))
            {
                _ = this.SetLectures();

            }
            else if (val.Equals("session"))
            {

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
      


    }
}
