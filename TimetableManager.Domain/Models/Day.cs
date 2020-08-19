using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.Domain.Models
{
    public class Day
    {
        public int DayId { get; set; }
        public string DayName { get; set; }
        public bool IsSelected { get; set; }
    }
}
