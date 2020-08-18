using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimetableManager.Domain.Models;

namespace TimetableManager.EntityFramework.Services
{
    public class LevelDataService : IDataService
    {
        public LevelDataService(TimetableManagerDbContext context) : base(context)
        {
        }

        public async Task<List<Level>> GetLevelsAsync()
        {
            return await base._context.Levels.ToListAsync();
        }
    }
}
