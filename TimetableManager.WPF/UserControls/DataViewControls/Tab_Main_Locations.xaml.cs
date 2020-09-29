using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TimetableManager.Domain.Models;
using TimetableManager.EntityFramework.Services;
using TimetableManager.WPF.UserControls.LecturerViewControls.DataGridModel;

namespace TimetableManager.WPF.Controls
{
    /// <summary>
    /// Interaction logic for Tab_Main_Locations.xaml
    /// </summary>
    public partial class Tab_Main_Locations : UserControl
    {
        private Room SelectedRoom;
        public int BuildingId { get; set; }
        public ObservableCollection<string> CenterNameList { get; private set; }
        public List<Center> CenterList { get; private set; }

        //
        public ObservableCollection<BuildingGridModel> BuildingDataList { get; private set; }

        public ObservableCollection<RoomGridModel> RoomDataList { get; private set; }

        public List<Building> BuildingList { get; private set; }

        public List<Room> roomList { get; private set; }

        //
        public ObservableCollection<string> BuildingNameList { get; private set; }

        public Tab_Main_Locations()
        {
            InitializeComponent();
            CenterNameList = new ObservableCollection<string>();
            _ = this.SetCenterList();

            BuildingDataList = new ObservableCollection<BuildingGridModel>();
            RoomDataList = new ObservableCollection<RoomGridModel>();
            BuildingNameList = new ObservableCollection<string>();

            _ = this.LoadBuildingData();
            _ = this.LoadRoomData();
            this.DataContext = this;

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
        private async Task SetCenterList()
        {
            CenterDataService centerDataService = new CenterDataService(new EntityFramework.TimetableManagerDbContext());

            CenterList = await centerDataService.GetCentersAsync();

            CenterList.ForEach(e =>
            {
                CenterNameList.Add(e.CenterName);
            });
        }

        private void SaveBuildingButton_Click(object sender, RoutedEventArgs e)
        {
            Building building = new Building();
            building.BuildingName = textBoxBuilding.Text.Trim();

            string centerName = CenComboBox.SelectedItem.ToString();

            BuildingDataService buildingDataService = new BuildingDataService(new EntityFramework.TimetableManagerDbContext());

            if(CenComboBox.IsEnabled)
            {
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
            } else
            {
                CenComboBox.IsEnabled = true;
                _ = buildingDataService.deleteBuilding(BuildingId);
                buildingDataService.AddBuilding(building, centerName).ContinueWith(result =>
                {
                    if (result != null)
                    {
                        MessageBox.Show("Building Updated!", "Success");
                    }
                    else
                    {
                        MessageBox.Show("Sorry! Error occured!", "Error");
                    }
                });
            }



            BuildingDataList.Clear();
            _ = this.LoadBuildingData();
        }

        private void EditButtonbuil_Click(object sender, RoutedEventArgs e)
        {
            LocationsTabControl.SelectedIndex = 1;
            BuildingGridModel building = (BuildingGridModel)dataGridBuilding.SelectedItem;

            _ = this.LoadDataForEditBuilding(building.BuildingId);
        }

        private async Task LoadDataForEditBuilding(int id)
        {
            BuildingDataService buildingdataservice = new BuildingDataService(new EntityFramework.TimetableManagerDbContext());
            BuildingId = id;
            Building bu = await buildingdataservice.GetBuildingById(id);
            textBoxBuilding.Text = bu.BuildingName;
            CenComboBox.SelectedItem = bu.Center.CenterName;
            CenComboBox.IsEnabled = false;
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

        private void EditButtonrro_Click(object sender, RoutedEventArgs e)
        {
            LocationsTabControl.SelectedIndex = 2;
            RoomGridModel room = (RoomGridModel)dataGridRoom.SelectedItem;
            _ = LoadDataForEditRoom(room.RoomId);
        }

        private async Task LoadDataForEditRoom(int id)
        {
            RoomDataService roomdataservice = new RoomDataService(new EntityFramework.TimetableManagerDbContext());

            SelectedRoom = await roomdataservice.GetRoomById(id);
            comboBoxcenter.SelectedItem = SelectedRoom.Center.CenterName;
            comboBoxcenter.IsEnabled = false;
            comboBoxbuild.SelectedItem = SelectedRoom.Building.BuildingName;
            comboBoxbuild.IsEnabled = false;
            textBoxrname.Text = SelectedRoom.RoomName;
            textBox1capacity.Text = SelectedRoom.Capacity.ToString();
        }

        private void DeleteButtonroom_Click(object sender, RoutedEventArgs e)
        {
            RoomGridModel roo = (RoomGridModel)dataGridRoom.SelectedItem;

            RoomDataService roomdataservice = new RoomDataService(new EntityFramework.TimetableManagerDbContext());

            roomdataservice.deleteRooms(roo.RoomId).ContinueWith(result =>
            {
                MessageBox.Show("Deleted");
            });

            _ = RoomDataList.Remove(roo);
        }

        private void comboBoxcenter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string center = comboBoxcenter.SelectedItem.ToString();

            CenterList.ForEach(e =>
            {
                if (e.CenterName.Equals(center))
                {
                    BuildingNameList.Clear();
                    e.Buildings.ForEach(b =>
                    {
                        BuildingNameList.Add(b.BuildingName);
                    });
                }
            });
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            RoomDataService roomDataService = new RoomDataService(new EntityFramework.TimetableManagerDbContext());

            if (comboBoxcenter.IsEnabled)
            {
                Room room = new Room();

                room.Capacity = Int32.Parse(textBox1capacity.Text.Trim());
                room.RoomName = textBoxrname.Text.Trim();

                string CName = comboBoxbuild.SelectedItem.ToString();
                string builName = comboBoxcenter.SelectedItem.ToString();

               

                roomDataService.AddRooms(room, builName, CName).ContinueWith(resultroom =>
                {
                    if (resultroom != null)
                    {
                        MessageBox.Show("Room and capacity Added!", "Success");
                    }
                    else
                    {
                        MessageBox.Show("Sorry! Error occured!", "Error");
                    }
                });
            } else
            {
                comboBoxcenter.IsEnabled = true;
                comboBoxbuild.IsEnabled = true;

                SelectedRoom.RoomName = textBoxrname.Text.Trim();
                SelectedRoom.Capacity = Int32.Parse(textBox1capacity.Text.Trim());

                _ = roomDataService.UpdateRoom(SelectedRoom);
            }
            

            RoomDataList.Clear();
            _ = LoadRoomData();
        }
    }
}
