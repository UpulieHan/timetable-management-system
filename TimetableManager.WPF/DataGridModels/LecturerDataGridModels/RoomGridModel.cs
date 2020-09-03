using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.WPF.UserControls.LecturerViewControls.DataGridModel
{
    public class RoomGridModel
    {
        public int RoomId { get; set; }

        public string RoomName { get; set; }

        public int Capacity { get; set; }

        public string BuildingName { get; set; }

        public string CenterName { get; set; }
    }
}
