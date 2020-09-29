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
    /// Interaction logic for Tab_Student_SubGroupNo.xaml
    /// </summary>
    public partial class Tab_Student_SubGroupNo : UserControl
    {
        SubGroupNumber subGroup = new SubGroupNumber();
        private bool isEditState = false;
        public ObservableCollection<SubGroupNumber> SubGroupNumberDataList { get; private set; }
        public List<SubGroupNumber> SubGroupNumberList { get; private set; }
        public Tab_Student_SubGroupNo()
        {
            InitializeComponent();
            this.DataContext = this;
            SubGroupNumberDataList = new ObservableCollection<SubGroupNumber>();
            _ = this.load();
        }
        public async Task load()
        {
            SubGroupNumberDataService subGroupNumberDataService = new SubGroupNumberDataService(new EntityFramework.TimetableManagerDbContext());
            SubGroupNumberList = await subGroupNumberDataService.GetSubGroupNumbers();
            SubGroupNumberList.ForEach(e =>
            {
                SubGroupNumber l = new SubGroupNumber();
                l.Id = e.Id;
                l.SubGroupNum = e.SubGroupNum;
                SubGroupNumberDataList.Add(l);
            });

        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SubGroupNumberDataService subgroupNumberDataService = new SubGroupNumberDataService(new EntityFramework.TimetableManagerDbContext());
            if (textBoxsubgrpNo.Text != "")
            {
                if(isEditState)
                {
                    subGroup.SubGroupNum = textBoxsubgrpNo.Text;
                    await subgroupNumberDataService.UpdateSubgroupNo(subGroup, subGroup.Id);
                    isEditState = false;
                } else
                {
                    SubGroupNumber subGroupNumber = new SubGroupNumber
                    {
                        SubGroupNum = textBoxsubgrpNo.Text
                    };
                    await subgroupNumberDataService.AddSubGroupNumber(subGroupNumber);
                }

                textBoxsubgrpNo.Clear();
            }
            else
            {
                MessageBox.Show("Insert a Sub-Group Number!!");
            }

            SubGroupNumberDataList.Clear();
            _ = this.load();
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            SubGroupNumber ys = (SubGroupNumber)dataGridsubgrpNo.SelectedItem;
            _ = LoadSubGroupForEdit(ys.Id);
        }

        private async Task LoadSubGroupForEdit(int id)
        {
            SubGroupNumberDataService subGroupNumberDataService = new SubGroupNumberDataService(new EntityFramework.TimetableManagerDbContext());

            subGroup = await subGroupNumberDataService.GetSubGroupNoById(id);
            textBoxsubgrpNo.Text = subGroup.SubGroupNum;
            isEditState = true;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            SubGroupNumber subgrpno = (SubGroupNumber)dataGridsubgrpNo.SelectedItem;

            SubGroupNumberDataService groupNumberData = new SubGroupNumberDataService(new EntityFramework.TimetableManagerDbContext());

            groupNumberData.DeleteSubGroupNumber(subgrpno.Id).ContinueWith(result =>
            {
                MessageBox.Show("Deleted");
            });

            _ = SubGroupNumberDataList.Remove(subgrpno);
        }
    }
}
