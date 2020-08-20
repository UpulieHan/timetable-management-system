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

namespace TimetableManager.WPF.UserControls.DataViewControls
{
    /// <summary>
    /// Interaction logic for Tab_Main_Tags_Update.xaml
    /// </summary>
    public partial class Tab_Main_Tags_Update : Window
    {
        public int Aid;
        Tag tag = new Tag();

        public Tab_Main_Tags_Update(int id)
        {
            InitializeComponent();
            this.DataContext = this;
            Aid = id;
            _ = loadData();
        }
        private async Task<bool> loadData()
        {
            TagDataService tagDataService = new TagDataService(new EntityFramework.TimetableManagerDbContext());

            Tag yst = await tagDataService.GetTagById(this.Aid);

            textBoxtag.Text = yst.TagName;
            return true;
        }
        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var tagDataService = new TagDataService(new EntityFramework.TimetableManagerDbContext());
            if (textBoxtag.Text != "")
            {
                tag.TagName = textBoxtag.Text;
                await tagDataService.UpdateTag(tag,Aid);
                MessageBox.Show("Update!!");

            }
            else
            {
                MessageBox.Show("Insert a Tag!!");
            }
        }
    }
}
