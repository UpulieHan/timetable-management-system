using System;
using System.Collections.Generic;
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

namespace TimetableManager.WPF.UserControls.StudentUserControls
{
    /// <summary>
    /// Interaction logic for Tab_Student_SubGroupNo_Update.xaml
    /// </summary>
    public partial class Tab_Student_SubGroupNo_Update : Window
    {
        public int Aid;
        SubGroupNumber subGroupNumber = new SubGroupNumber();

        public Tab_Student_SubGroupNo_Update(int id)
        {
            InitializeComponent();
            this.DataContext = this;
            Aid = id;
            _ = loadData();
        }
        private async Task<bool> loadData()
        {
            SubGroupNumberDataService subGroupNumberDataService = new SubGroupNumberDataService(new EntityFramework.TimetableManagerDbContext());

            SubGroupNumber yst = await subGroupNumberDataService.GetSubGroupNoById(this.Aid);

            textBoxsubgrpNo.Text = yst.SubGroupNum;
            return true;
        }
        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SubGroupNumberDataService subgroupNumberDataService = new SubGroupNumberDataService(new EntityFramework.TimetableManagerDbContext());
            if (textBoxsubgrpNo.Text != "")
            {
                subGroupNumber.SubGroupNum = textBoxsubgrpNo.Text;
                await subgroupNumberDataService.UpdateSubgroupNo(subGroupNumber,Aid);
                MessageBox.Show("Update!!");

            }
            else
            {
                MessageBox.Show("Insert a Sub-Group Number!!");
            }
        }
    }
}
