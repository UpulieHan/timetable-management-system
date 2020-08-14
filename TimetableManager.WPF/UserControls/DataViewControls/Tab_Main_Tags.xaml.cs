using System;
using System.Collections.Generic;
using System.Text;
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
using TimetableManager.Domain.Services;
using TimetableManager.EntityFramework.Services;

namespace TimetableManager.WPF.Controls
{
    /// <summary>
    /// Interaction logic for Tab_Main_Tags.xaml
    /// </summary>
    public partial class Tab_Main_Tags : UserControl
    {
        Tag tag = new Tag();
        public Tab_Main_Tags()
        {
            InitializeComponent();
            List<Tag> users = new List<Tag>();
            users.Add(new Tag() { Id = 1, TagName = "John Doe"});
            users.Add(new Tag() { Id = 2, TagName = "Jane Doe"});
            users.Add(new Tag() { Id = 3, TagName = "Sammy Doe"});

            dataGrid.ItemsSource = users;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            IDataService<Tag> TagDataService = new GenericDataService<Tag>(new EntityFramework.TimetableManagerDbContextFactory());
            if (textBoxtag.Text == "")
            {
                MessageBox.Show("Plese Enter a tag!..", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                tag.TagName = textBoxtag.Text;
                TagDataService.Create(tag);
            }
        }

        
    }
}
