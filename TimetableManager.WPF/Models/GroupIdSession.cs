using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.Domain.Models
{
    public class GroupIdSession
    {
        public int GroupId { get; set; }
        public GroupId Group { get; set; }
        public int SessionId { get; set; }
        public Session Session { get; set; }
    }
}
