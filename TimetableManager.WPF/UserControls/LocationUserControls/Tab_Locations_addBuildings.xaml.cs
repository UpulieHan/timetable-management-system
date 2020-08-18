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
using System.Windows.Threading;
using TimetableManager.Domain.Models;
using TimetableManager.EntityFramework.Services;

namespace TimetableManager.WPF.UserControls.LocationUserControls
{
    /// <summary>
    /// Interaction logic for Tab_Locations_addBuildings.xaml
    /// </summary>
    public partial class Tab_Locations_addBuildings : UserControl
    {

        public ObservableCollection<string> CenterNameList { get; private set; }
        public List<Center> CenterList { get; private set; }
        public Tab_Locations_addBuildings()
        {
            InitializeComponent();
            CenterNameList = new ObservableCollection<string>();

            this.DataContext = this;

            _ = this.Dispatcher.Invoke(
                DispatcherPriority.ApplicationIdle,
                new Action(() =>
                {

                    _ = this.SetCenterList();

                }));
        }

        private async Task SetCenterList()
        {
            CenterDataService centerDataService = new CenterDataService(new EntityFramework.TimetableManagerDbContext());

            CenterList = await centerDataService.GetCentersAsync();

            CenterList.ForEach(e =>
            {
                CenterNameList.Add(e.CenterName);
            });
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
