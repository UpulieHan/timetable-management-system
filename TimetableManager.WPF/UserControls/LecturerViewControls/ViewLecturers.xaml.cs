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

namespace TimetableManager.WPF.UserControls.LecturerViewControls
{
    /// <summary>
    /// Interaction logic for ViewLecturers.xaml
    /// </summary>
    public partial class ViewLecturers : UserControl
    {
        public ObservableCollection<LecturerGridModel> LecturerDataList { get; private set; }
        public List<Lecturer> LecturerList { get; private set; }
        public ViewLecturers()
        {
            InitializeComponent();
            this.DataContext = this;

            LecturerDataList = new ObservableCollection<LecturerGridModel>();

            _ = this.LoadLecturerData();
        }

        private async Task LoadLecturerData()
        {
            LecturerDataService lecturerDataService = new LecturerDataService(new EntityFramework.TimetableManagerDbContext());

            LecturerList = await lecturerDataService.GetLecturersAsync();

            LecturerList.ForEach(e =>
            {
                LecturerGridModel lecturerObj = new LecturerGridModel();
                lecturerObj.EmployeeId = e.EmployeeId;
                lecturerObj.EmployeeName = e.EmployeeName;
                lecturerObj.FacultyName = e.Faculty.FacultyName;
                lecturerObj.DepartmentName = e.Department.DepartmentName;
                lecturerObj.CenterName = e.Center.CenterName;
                lecturerObj.BuildingName = e.Building.BuildingName;
                lecturerObj.LevelName = e.Level.LevelName;
                lecturerObj.Rank = e.Rank;

                LecturerDataList.Add(lecturerObj);
            });
        }
    }
}
