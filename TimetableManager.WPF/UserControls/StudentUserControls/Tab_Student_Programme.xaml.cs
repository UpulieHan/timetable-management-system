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
    /// Interaction logic for Tab_Student_Programme.xaml
    /// </summary>
    public partial class Tab_Student_Programme : UserControl
    {
        Programme programme = new Programme();
        public ObservableCollection<Programme> programmeDataList { get; private set; }
        public List<Programme> programmeList { get; private set; }
        public Tab_Student_Programme()
        {
            InitializeComponent();
            this.DataContext = this;
            programmeDataList = new ObservableCollection<Programme>();
            _ = this.load();
        }
        public async Task load()
        {
            ProgrammeDataService programmeDataService = new ProgrammeDataService(new EntityFramework.TimetableManagerDbContext());
            programmeList = await programmeDataService.GetProgramme();
            programmeList.ForEach(e =>
            {
                Programme l = new Programme();
                l.ProgrammeId = e.ProgrammeId;
                l.ProgrammeFullName = e.ProgrammeFullName;
                l.ProgrammeShortName = e.ProgrammeShortName;
                programmeDataList.Add(l);
            });

        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var programmeDataService = new ProgrammeDataService(new EntityFramework.TimetableManagerDbContext());
            if (textBoxfullname.Text != "" || textBoxshortame.Text != "")
            {
                programme.ProgrammeFullName = textBoxfullname.Text;
                programme.ProgrammeShortName = textBoxshortame.Text;
                await programmeDataService.AddProgramme(programme);
            }
            else
            {
                MessageBox.Show("fill all fields!!");
            }
        }
    }
}
