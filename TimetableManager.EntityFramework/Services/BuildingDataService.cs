using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<bool> AddBuilding(Building building)
        {
            _context.Buildings.Add(building);
            int resultbuilding = await _context.SaveChangesAsync();

            if (resultbuilding > 0)
            {
                return true;
            }

            return false;

        }

        public async Task<List<Building>> GetBuildingsAsync()
        {
            return await _context.Buildings.Include(e => e.Center).ToListAsync();
        }

    }
}
