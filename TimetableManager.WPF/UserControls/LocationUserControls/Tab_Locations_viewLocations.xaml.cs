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
using TimetableManager.WPF.UserControls.LecturerViewControls.DataGridModel;

namespace TimetableManager.WPF.UserControls.LocationUserControls
{
    /// <summary>
    /// Interaction logic for Tab_Locations_viewLocations.xaml
    /// </summary>
    public partial class Tab_Locations_viewLocations : UserControl
    {
        public ObservableCollection<BuildingGridModel> BuildingDataList { get; private set; }

        public ObservableCollection<RoomGridModel> RoomDataList { get; private set; }

        public List<Building> BuildingList { get; private set; }

        public List<Room> roomList { get; private set; }

        public Tab_Locations_viewLocations()
        {
            InitializeComponent();
            this.DataContext = this;
            BuildingDataList = new ObservableCollection<BuildingGridModel>();
            RoomDataList = new ObservableCollection<RoomGridModel>();


            _ = this.LoadBuildingData();
            _ = this.LoadRoomData();
        }


        private async Task LoadBuildingData()
        {
            BuildingDataService buildingdataservice = new BuildingDataService(new EntityFramework.TimetableManagerDbContext());

            BuildingList = await buildingdataservice.GetBuildingsAsync();

            BuildingList.ForEach(f =>
            {
                BuildingGridModel buildingobj = new BuildingGridModel();

                buildingobj.BuildingId = f.BuildingId;
                buildingobj.BuildingName = f.BuildingName;
                buildingobj.CenterName = f.Center.CenterName;

                BuildingDataList.Add(buildingobj);
            });
        }

        private async Task LoadRoomData()
        {
            RoomDataService roomdataservice = new RoomDataService(new EntityFramework.TimetableManagerDbContext());

            roomList = await roomdataservice.GetRoomAsync();

            roomList.ForEach(g => {

                RoomGridModel roomobj = new RoomGridModel();

                roomobj.RoomId = g.RoomId;
                roomobj.RoomName = g.RoomName;
                roomobj.Capacity = g.Capacity;
                roomobj.BuildingName = g.Building.BuildingName;
                roomobj.CenterName = g.Center.CenterName;

                RoomDataList.Add(roomobj);

            });
        }


          private void EditButtonbuilding_Click(object sender, RoutedEventArgs e)
         {
              BuildingGridModel bui =  (BuildingGridModel)dataGridBuilding.SelectedItem;
              Tab_Location_update tablocup = new Tab_Location_update(bui.BuildingId);
              tablocup.Show();

          // Close current main data window. Hard coded. Need to be changed
            //  Application.Current.Windows[2].Close();

          }
        
        private void DeleteButtonbuilding_Click(object sender, RoutedEventArgs e)
        {
            BuildingGridModel build = (BuildingGridModel)dataGridBuilding.SelectedItem;

            BuildingDataService buildingdataservice = new BuildingDataService(new EntityFramework.TimetableManagerDbContext());

            buildingdataservice.deleteBuilding(build.BuildingId).ContinueWith(result =>
            {
                MessageBox.Show("Deleted");
            });

            _ = BuildingDataList.Remove(build);
        }


        private void EditButtonrooms_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Edit button clicked");
        }


        private void DeleteButtonrooms_Click(object sender, RoutedEventArgs e)
        {
            RoomGridModel roo = (RoomGridModel)dataGridRoom.SelectedItem;

            RoomDataService roomdataservice = new RoomDataService(new EntityFramework.TimetableManagerDbContext());

            roomdataservice.deleteRooms(roo.RoomId).ContinueWith(result =>
            {
                MessageBox.Show("Deleted");
            });

            _ = RoomDataList.Remove(roo);
        }
    }
}
