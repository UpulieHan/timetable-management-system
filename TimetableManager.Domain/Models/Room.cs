﻿namespace TimetableManager.Domain.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int Capacity { get; set; }
        public Building Building { get; set; }
        public Center Center { get; set; }
    }
}
