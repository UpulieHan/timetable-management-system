using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.EntityFramework.Services
{
    public class RoomDataService
    {
        private readonly TimetableManagerDbContext _context;


        public RoomDataService(TimetableManagerDbContext context)
        {
            _context = context;
        }

       /* public async Task<bool> AddRooms(Rooms rooms)
        {
            _context.Faculties.Add(rooms);
            int resRoom = await _context.SaveChangesAsync();

            if (resRoom > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> AddCapacity(Rooms rooms)
        {
            _context.Faculties.Add(rooms);
            int resRoom = await _context.SaveChangesAsync();

            if (resRoom > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<List<Rooms>> GetFacultiesAsync()
        {
            return await _context.Faculties.Include(e => e.Romes).ToListAsync();
        }
       */
    }

    
}
