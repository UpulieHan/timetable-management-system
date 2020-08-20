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
    /// Interaction logic for Tab_Student_GroupNo_Update.xaml
    /// </summary>
    public partial class Tab_Student_GroupNo_Update : Window
    {
        public int Aid;
        GroupNumber groupNumber = new GroupNumber();

        public Tab_Student_GroupNo_Update(int id)
        {
            InitializeComponent();
            this.DataContext = this;
            Aid = id;
            _ = loadData();
        }
        private async Task<bool> loadData()
        {
            GroupNumberDataService groupNumberDataService = new GroupNumberDataService(new EntityFramework.TimetableManagerDbContext());

            GroupNumber yst = await groupNumberDataService.GetGroupNoById(this.Aid);

            textBoxgrpNo.Text = yst.GroupNum;
            return true;
        }
        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            GroupNumberDataService groupNumberDataService = new GroupNumberDataService(new EntityFramework.TimetableManagerDbContext());
            if (textBoxgrpNo.Text != "")
            {
                groupNumber.GroupNum = textBoxgrpNo.Text;
                await groupNumberDataService.UpdateGroupNo(groupNumber,Aid);
                MessageBox.Show("Updated!!");

            }
            else
            {
                MessageBox.Show("Insert a Group Number!!");
            }
        }
    }
}
