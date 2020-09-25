﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.Domain.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public List<Lecturer> Lecturers { get; set; }
        public Subject Subject { get; set; }
        public Tag Tag { get; set; }
        public List<GroupId> GroupIds { get; set; }
        public int StudentCount { get; set; }
        public int Duration { get; set; }
    }
}
