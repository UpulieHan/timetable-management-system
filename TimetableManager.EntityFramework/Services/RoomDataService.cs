using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableManager.Domain.Models;

namespace TimetableManager.EntityFramework.Services
{
    public class RoomDataService
    {
        private readonly TimetableManagerDbContext _context;


        public RoomDataService(TimetableManagerDbContext context)
        {
            _context = context;
        }


        public Task<int> AddRooms(Room rooms, string cName, string buildName)
        {

            var center = _context.Centers.Include(e => e.Rooms).Single(e => e.CenterName == cName);
            center.Rooms.Add(rooms);

            var building = _context.Buildings.Include(e => e.Rooms).Single(e => e.BuildingName == buildName);
            building.Rooms.Add(rooms);

            return _context.SaveChangesAsync();
        }

        
         public async Task<List<Room>> GetRoomAsync()
         {
             return await _context.Rooms.Include(e => e.Center).ToListAsync();
         }
        
    }

    
}
