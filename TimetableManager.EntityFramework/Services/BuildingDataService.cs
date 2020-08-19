using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

       /* public async Task<List<Building>> GetBuildingsAsync()
        {
            return await _context.Buildings.Include(e => e.Center).ToListAsync();
        }*/

    }
}
