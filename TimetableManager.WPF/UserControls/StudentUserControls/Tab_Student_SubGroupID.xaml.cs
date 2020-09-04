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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimetableManager.Domain.Models;
using TimetableManager.EntityFramework.Services;

namespace TimetableManager.WPF.UserControls.StudentUserControls
{
    /// <summary>
    /// Interaction logic for Tab_Student_SubGroupID.xaml
    /// </summary>
    public partial class Tab_Student_SubGroupID : UserControl
    {
        SubGroupId grpid = new SubGroupId();
        public ObservableCollection<SubGroupId> SubGroupIdDataList { get; private set; }
        public List<SubGroupId> SubGroupIdList { get; private set; }

        public Tab_Student_SubGroupID()
        {
            InitializeComponent();
            this.DataContext = this;
            SubGroupIdDataList = new ObservableCollection<SubGroupId>();
            _ = this.load();
        }

       
        public async Task load()
        {
            SubGroupIdDataService groupIdDataService = new SubGroupIdDataService(new EntityFramework.TimetableManagerDbContext());
            SubGroupIdList = await groupIdDataService.GetSubGroupId();
            SubGroupIdList.ForEach(e =>
            {
                SubGroupId l = new SubGroupId();
                l.Id = e.Id;
                l.SubGroupID = e.SubGroupID;
                SubGroupIdDataList.Add(l);
            });

        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            SubGroupId groupId = (SubGroupId)dataGridSubGroupID.SelectedItem;

            SubGroupIdDataService groupIdDataService = new SubGroupIdDataService(new EntityFramework.TimetableManagerDbContext());

            groupIdDataService.DeleteSubGroupId(groupId.Id).ContinueWith(result =>
            {
                MessageBox.Show("Deleted");
            });

            _ = SubGroupIdDataList.Remove(groupId);
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

            SubGroupNumberDataService subGroupNumberDataService = new SubGroupNumberDataService(new EntityFramework.TimetableManagerDbContext());
            List<SubGroupNumber> SubGroupNumberList = await subGroupNumberDataService.GetSubGroupNumbers();
            List<string> SubGroupNameList = new List<string>();
            SubGroupNumberList.ForEach(e =>
            {
                SubGroupNameList.Add(e.SubGroupNum);
            });

            List<string> GeneratedList = new List<string>();

            YearNameList.ForEach(y =>
            {
                ProgrammeNameList.ForEach(p =>
                {
                    GroupNameList.ForEach(g =>
                    {
                        SubGroupNameList.ForEach(s =>
                        {
                            string id = String.Concat(y, ".", p, ".", g, ".", s);
                            GeneratedList.Add(id);
                        });
                    });
                });
            });

            GeneratedList.ForEach(e =>
            {
                SubGroupIdDataList.Add(new SubGroupId
                {
                    SubGroupID = e
                });
            });
        }

        private void btnSubGrpIDs_Click(object sender, RoutedEventArgs e)
        {
            _ = this.LoadDataForGenerate();
        }
    }
}
