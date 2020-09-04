using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TimetableManager.Domain.Models;
using TimetableManager.EntityFramework.Services;

namespace TimetableManager.WPF.UserControls.StudentUserControls
{
    /// <summary>
    /// Interaction logic for Tab_Student_GroupID.xaml
    /// </summary>
    public partial class Tab_Student_GroupID : UserControl
    {
        GroupId grpid = new GroupId();
        public ObservableCollection<GroupId> GroupIdDataList { get; private set; }
        public List<GroupId> GroupIdList { get; private set; }
        public Tab_Student_GroupID()
        {
            InitializeComponent();
            this.DataContext = this;
            GroupIdDataList = new ObservableCollection<GroupId>();
            _ = this.load();
        }
        public async Task load()
        {
            GroupIdDataService groupIdDataService = new GroupIdDataService(new EntityFramework.TimetableManagerDbContext());
            GroupIdList = await groupIdDataService.GetGroupId();
            GroupIdList.ForEach(e =>
            {
                GroupId l = new GroupId();
                l.Id = e.Id;
                l.GroupID = e.GroupID;
                GroupIdDataList.Add(l);
            });

        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            GroupId groupId = (GroupId)dataGridGroupID.SelectedItem;

            GroupIdDataService groupIdDataService = new GroupIdDataService(new EntityFramework.TimetableManagerDbContext());

            groupIdDataService.DeleteGroupId(groupId.Id).ContinueWith(result =>
            {
                MessageBox.Show("Deleted");
            });

            _ = GroupIdDataList.Remove(groupId);
        }

        private void btnGrpID_Click(object sender, RoutedEventArgs e)
        {
            _ = LoadDataForGenerate();
        }

        private async Task LoadDataForGenerate()
        {
            Year_SemesterDataService year_SemesterDataService = new Year_SemesterDataService(new EntityFramework.TimetableManagerDbContext());
            List<Year_Semester> YsList = await year_SemesterDataService.GetYs();
            List<string> YearNameList = new List<string>();
            YsList.ForEach(e =>
            {
                YearNameList.Add(e.YsShortName);
            });

            ProgrammeDataService programmeDataService = new ProgrammeDataService(new EntityFramework.TimetableManagerDbContext());
            List<Programme> programmeList = await programmeDataService.GetProgramme();
            List<string> ProgrammeNameList = new List<string>();
            programmeList.ForEach(e =>
            {
                ProgrammeNameList.Add(e.ProgrammeShortName);
            });

            GroupNumberDataService groupNumberDataService = new GroupNumberDataService(new EntityFramework.TimetableManagerDbContext());
            List<GroupNumber> GroupNumberList = await groupNumberDataService.GetGroupNumbers();
            List<string> GroupNameList = new List<string>();
            GroupNumberList.ForEach(e =>
            {
                GroupNameList.Add(e.GroupNum);
            });

            List<string> GeneratedList = new List<string>();

            YearNameList.ForEach(y =>
            {
                ProgrammeNameList.ForEach(p =>
                {
                    GroupNameList.ForEach(g =>
                    {
                        string id = String.Concat(y, ".", p, ".", g);
                        GeneratedList.Add(id);
                    });
                });
            });

            GeneratedList.ForEach(e =>
            {
                GroupIdDataList.Add(new GroupId
                {
                    GroupID = e
                });
            });
        }
    }
}
