using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TimetableManager.Domain.Models;
using TimetableManager.EntityFramework.Services;

namespace TimetableManager.WPF.UserControls.StudentUserControls
{
    /// <summary>
    /// Interaction logic for Tab_Student_GroupNo.xaml
    /// </summary>
    public partial class Tab_Student_GroupNo : UserControl
    {
        GroupNumber groupNumber = new GroupNumber();
        private bool isEditState = false;
        public ObservableCollection<GroupNumber> GroupNumberDataList { get; private set; }
        public List<GroupNumber> GroupNumberList { get; private set; }
        public Tab_Student_GroupNo()
        {
            InitializeComponent();
            this.DataContext = this;
            GroupNumberDataList = new ObservableCollection<GroupNumber>();
            _ = this.load();
        }
        public async Task load()
        {
            GroupNumberDataService groupNumberDataService = new GroupNumberDataService(new EntityFramework.TimetableManagerDbContext());
            GroupNumberList = await groupNumberDataService.GetGroupNumbers();
            GroupNumberList.ForEach(e =>
            {
                GroupNumber l = new GroupNumber();
                l.Id = e.Id;
                l.GroupNum = e.GroupNum;
                GroupNumberDataList.Add(l);
            });

        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            GroupNumberDataService groupNumberDataService = new GroupNumberDataService(new EntityFramework.TimetableManagerDbContext());
            if (textBoxgrpNo.Text != "")
            {
                if(isEditState)
                {
                    isEditState = false;

                    groupNumber.GroupNum = textBoxgrpNo.Text;

                    await groupNumberDataService.UpdateGroupNo(groupNumber, groupNumber.Id);
                } else
                {
                    GroupNumber groupNumber = new GroupNumber
                    {
                        GroupNum = textBoxgrpNo.Text
                    };

                    await groupNumberDataService.AddGroupNumber(groupNumber);
                }

                textBoxgrpNo.Clear();
            }
            else
            {
                MessageBox.Show("Insert a Group Number!!");
            }

            this.GroupNumberDataList.Clear();
            _ = load();
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            GroupNumber ys = (GroupNumber)dataGridgrpNo.SelectedItem;
            _ = LoadGroupForEdit(ys.Id);
        }

        private async Task LoadGroupForEdit(int id)
        {
            GroupNumberDataService groupNumberDataService = new GroupNumberDataService(new EntityFramework.TimetableManagerDbContext());

            groupNumber = await groupNumberDataService.GetGroupNoById(id);

            textBoxgrpNo.Text = groupNumber.GroupNum;

            isEditState = true;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            GroupNumber grpno = (GroupNumber)dataGridgrpNo.SelectedItem;

            GroupNumberDataService groupNumberData = new GroupNumberDataService(new EntityFramework.TimetableManagerDbContext());

            groupNumberData.DeleteGroupNumbers(grpno.Id).ContinueWith(result =>
            {
                MessageBox.Show("Deleted");
            });

            _ = GroupNumberDataList.Remove(grpno);
        }
    }
}
