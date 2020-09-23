using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using TimetableManager.Domain.Models;
using TimetableManager.EntityFramework.Services;

namespace TimetableManager.WPF.StatisticsTimetableDataControls.StatisticUserControl
{
    /// <summary>
    /// Interaction logic for Tab_Stat_lecturer.xaml
    /// </summary>
    public partial class Tab_Stat_lecturer : UserControl
    {
        public List<Lecturer> LecturesList { get; private set; }
        public Hashtable HashTable = new Hashtable();
        public List<LecturerStatGrid> LecturerStatList = new List<LecturerStatGrid>();

        public Tab_Stat_lecturer()
        {
            InitializeComponent();

            HashTable.Add("Professor", 0);
            HashTable.Add("Assistant Professor", 0);
            HashTable.Add("Senior Lecturer(HG)", 0);
            HashTable.Add("Senior Lecturer", 0);
            HashTable.Add("Lecturer", 0);
            HashTable.Add("Assistant Lecturer", 0);
            HashTable.Add("Instructors", 0);

            _ = this.LoadLecturerData();

            dataGrid.ItemsSource = LecturerStatList;
        }

        private async Task LoadLecturerData()
        {
            LecturerDataService lecturerDataService = new LecturerDataService(new EntityFramework.TimetableManagerDbContext());

            LecturesList = await lecturerDataService.GetLecturersAsync();

            LecturesList.ForEach(e =>
            {
                HashTable[e.Level.LevelName] = (int)HashTable[e.Level.LevelName] + 1;
            });

            foreach(DictionaryEntry entry in HashTable)
            {
                LecturerStatList.Add(new LecturerStatGrid { Rank = (string)entry.Key, Count = (int)entry.Value });
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }

    public class LecturerStatGrid
    {
        public string Rank { get; set; }
        public int Count { get; set; }
    }
}
