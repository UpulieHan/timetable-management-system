using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetableManager.Domain.Models;

namespace TimetableManager.EntityFramework.Services
{
    public class BuildingDataService
    {
        private readonly TimetableManagerDbContext _context;

        public BuildingDataService(TimetableManagerDbContext context)
        {
            _context = context;
        }

        public Task<int> AddBuilding(Building Building, string cName)
        {
            
            var center = _context.Centers.Include(e => e.Buildings).Single(e => e.CenterName == cName);
            center.Buildings.Add(Building);

            return _context.SaveChangesAsync();
        }

        public async Task<List<Building>> GetBuildingsAsync()
        {
            return await _context.Buildings.Include(e => e.Center)
                                            .ToListAsync();
        }

        public async Task<Building> GetBuildingById(int id)
        {
            return await _context.Buildings.Where(e => e.BuildingId == id)
                                
                                .Include(e => e.Center)
                                
                                .FirstAsync();
        }



        public async Task<int> deleteBuilding(int id)
        {
            var building = _context.Buildings.Where(e => e.BuildingId == id).First();

            _context.Buildings.Remove(building);

            return await _context.SaveChangesAsync();
        }


        public async Task<int> UpdateBuilding(Building building, string cName)
        {
            await this.deleteBuilding(building.BuildingId);

            var center = _context.Centers.Include(e => e.Buildings).Single(e => e.CenterName == cName);
            center.Buildings.Add(building);

            return await _context.SaveChangesAsync();
        }

    }
}
