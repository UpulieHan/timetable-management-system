using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.Domain.Models
{
    public class SubGroupIdSession
    {
        public int SubGroupId { get; set; }
        public SubGroupId SubGroup { get; set; }
        public int SessionId { get; set; }
        public Session Session { get; set; }
    }
}
