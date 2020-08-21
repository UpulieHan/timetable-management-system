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
    /// Interaction logic for Tab_Location_update.xaml
    /// </summary>
    public partial class Tab_Location_update : Window
    {
        public ObservableCollection<string> CenterNameList { get; private set; }
        public List<Center> CenterList { get; private set; }

        public int BuildingId { get; set; }

        public Tab_Location_update(int id)
        {
            InitializeComponent();

            this.DataContext = this;

            CenterNameList = new ObservableCollection<string>();

            BuildingId = id;

            _ = this.Dispatcher.Invoke(
                DispatcherPriority.ApplicationIdle,
                new Action(() =>
                {

                    _ = this.SetCenterList();
                    _ = this.LoadBuildingData();

                }));
        }

        private async Task<bool> LoadBuildingData()
        {
            BuildingDataService buildingdataservice = new BuildingDataService(new EntityFramework.TimetableManagerDbContext());

            Building bu = await buildingdataservice.GetBuildingById(this.BuildingId);
            textBoxBuilding.Text = bu.BuildingName;
            CenComboBox.SelectedItem = bu.Center.CenterName;

            return true;
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

        private void savebuildingbutton_Click(object sender, RoutedEventArgs e)
        {
            Building building = new Building();

            building.BuildingName = textBoxBuilding.Text.Trim();

            string centerName = CenComboBox.SelectedItem.ToString();

            BuildingDataService buildingDataService = new BuildingDataService(new EntityFramework.TimetableManagerDbContext());

            buildingDataService.AddBuilding(building, centerName).ContinueWith(result =>
            {
                if (result != null)
                {
                    MessageBox.Show("Building Added!", "Success");
                }
                else
                {
                    MessageBox.Show("Sorry! Error occured!", "Error");
                }
            });
        }
    }
}
