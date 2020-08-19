using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TimetableManager.Domain.Models
{
    public class DaysAndHours
    {
        public int Id { get; set; }
        public int NoOfDays { get; set; }
        public int Hours { get; set; }
        public int Mins { get; set; }
        public int TimeSlot { get; set; }
    }
}
