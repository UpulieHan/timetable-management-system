using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        private bool isEditState = false;
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
                if(isEditState)
                {
                    tag.TagName = textBoxtag.Text;
                    await tagDataService.UpdateTag(tag, tag.TagId);
                    isEditState = false;
                }
                else
                {
                    Tag newTag = new Tag
                    {
                        TagName = textBoxtag.Text
                    };
                    await tagDataService.AddTag(newTag);
                }
                textBoxtag.Clear();
            }
            else 
            {
                MessageBox.Show("Insert a Tag!!");
            }

            TagDataList.Clear();
            _ = load();
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
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Tag ys = (Tag)dataGridtag.SelectedItem;
            //Tab_Main_Tags_Update updateysWindow = new Tab_Main_Tags_Update(ys.TagId);
            // updateysWindow.Show();

            // Close current main data window. Hard coded. Need to be changed
            // Application.Current.Windows[2].Close();
            _ = LoadTagDataForEdit(ys.TagId);
        }

        private async Task LoadTagDataForEdit(int id)
        {
            TagDataService tagDataService = new TagDataService(new EntityFramework.TimetableManagerDbContext());
            tag = await tagDataService.GetTagById(id);
            textBoxtag.Text = tag.TagName;
            isEditState = true;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Tag tag = (Tag)dataGridtag.SelectedItem;

            TagDataService tagDataService = new TagDataService(new EntityFramework.TimetableManagerDbContext());

            tagDataService.DeleteTag(tag.TagId).ContinueWith(result =>
            {
                MessageBox.Show("Deleted");
            });

            _ = TagDataList.Remove(tag);
        }



    }
}
