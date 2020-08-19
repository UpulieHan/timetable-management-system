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

namespace TimetableManager.WPF.Controls
{
    /// <summary>
    /// Interaction logic for Tab_Main_Tags.xaml
    /// </summary>
    public partial class Tab_Main_Tags : UserControl
    {

        Tag tag = new Tag();
        public ObservableCollection<Tag> TagDataList { get; private set; }
        public List<Tag> TagList { get; private set; }
        public Tab_Main_Tags()
        {
            InitializeComponent();
            this.DataContext = this;
           TagDataList = new ObservableCollection<Tag>();
            _ = this.load();
        }
        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var tagDataService = new TagDataService(new EntityFramework.TimetableManagerDbContext());
            if (textBoxtag.Text != "")
            {
                tag.TagName = textBoxtag.Text;
                await tagDataService.AddTag(tag);
            }
            else 
            {
                MessageBox.Show("Insert a Tag!!");
            }
        }
        public async Task load()
        {
            TagDataService tagDataService = new TagDataService(new EntityFramework.TimetableManagerDbContext());
            TagList = await tagDataService.GetTags();
            TagList.ForEach(e =>
            {
                Tag l = new Tag();
                l.TagId = e.TagId;
                l.TagName = e.TagName;
                TagDataList.Add(l);
            });

        }

        
    }
}
