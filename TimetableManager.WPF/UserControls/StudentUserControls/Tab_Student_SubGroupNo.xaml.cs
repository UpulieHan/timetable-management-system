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
    /// Interaction logic for Tab_Student_SubGroupNo.xaml
    /// </summary>
    public partial class Tab_Student_SubGroupNo : UserControl
    {
        SubGroupNumber subGroup = new SubGroupNumber();
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
                subGroup.SubGroupNum = textBoxsubgrpNo.Text;
                await subgroupNumberDataService.AddSubGroupNumber(subGroup);
            }
            else
            {
                MessageBox.Show("Insert a Sub-Group Number!!");
            }
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            SubGroupNumber ys = (SubGroupNumber)dataGridsubgrpNo.SelectedItem;
            Tab_Student_SubGroupNo_Update updateysWindow = new Tab_Student_SubGroupNo_Update(ys.Id);
            updateysWindow.Show();

            // Close current main data window. Hard coded. Need to be changed
            Application.Current.Windows[2].Close();
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
