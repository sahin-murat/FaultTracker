using FaultTracker.Business.Interfaces;
using FaultTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaultTracker.Business.Services
{
    public class MaintenanceHistoryService : CreatedRepositoryBase<MaintenanceHistory>, IMaintenanceHistoryRepository
    {
        public MaintenanceHistoryService(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<MaintenanceHistory>> GetAllWithAllRelationsAsync()
        {
            return await _entity.Include(e => e.ActionType).ToListAsync();
        }

        public async Task<MaintenanceHistory> GetWithAllRelationsAsync(int id)
        {
            return await _entity.Include(e => e.ActionType).Where(e => e.ID == id).FirstOrDefaultAsync();
        }
    }
}
