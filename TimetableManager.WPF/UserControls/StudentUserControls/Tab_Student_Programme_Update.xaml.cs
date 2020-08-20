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
    /// Interaction logic for Tab_Student_Programme_Update.xaml
    /// </summary>
    public partial class Tab_Student_Programme_Update : Window
    {
        public int Aid;
        Programme programme = new Programme();

        public Tab_Student_Programme_Update(int id)
        {
            InitializeComponent();
            this.DataContext = this;
            Aid = id;
            _ = loadData();
        }
        private async Task<bool> loadData()
        {
            ProgrammeDataService programmeDataService = new ProgrammeDataService(new EntityFramework.TimetableManagerDbContext());

            Programme yst = await programmeDataService.GetProgrammeById(this.Aid);

            textBoxshortame.Text = yst.ProgrammeShortName;
            textBoxfullname.Text = yst.ProgrammeFullName;
            return true;
        }
        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var programmeDataService = new ProgrammeDataService(new EntityFramework.TimetableManagerDbContext());
            if (textBoxfullname.Text != "" || textBoxshortame.Text != "")
            {
                programme.ProgrammeFullName = textBoxfullname.Text;
                programme.ProgrammeShortName = textBoxshortame.Text;
                await programmeDataService.UpdateProgramme(programme,Aid);
                MessageBox.Show("Update!!");


            }
            else
            {
                MessageBox.Show("fill all fields!!");
            }
        }
    }
}
