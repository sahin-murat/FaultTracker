using FaultTracker.Business.Interfaces;
using FaultTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaultTracker.Business.Services
{
    public class MaintenanceService : CreatedRepositoryBase<Maintenance>, IMaintenanceRepository
    {
        public MaintenanceService(DbContext context) : base(context)
        {
        }

        /// <summary>
        /// Get Maintenance details with all the relations
        /// <para>Includes: Vehicle, User, Status, PictureGroup, MaintenanceHistories</para>
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Maintenance>> GetAllWithAllRelationsAsync()
        {
            return await _entity
                    .Include(e => e.Vehicle)
                        .ThenInclude(s => s.VehicleType)
                    .Include(e => e.User)
                    .Include(e => e.PictureGroup)
                    .Include(e => e.Status)
                    .Include(e => e.MaintenanceHistories)
                        .ThenInclude(s => s.ActionType)
                    .ToListAsync();
        }

        /// <summary>
        /// Get All Maintenance details with all the relations
        /// <para>Includes: Vehicle, User, Status, PictureGroup, MaintenanceHistories</para>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Maintenance> GetWithAllRelationsAsync(int id)
        {
            return await _entity
                    .Include(e => e.Vehicle)
                        .ThenInclude(s => s.VehicleType)
                    .Include(e => e.User)
                    .Include(e => e.PictureGroup)
                    .Include(e => e.Status)
                    .Include(e => e.MaintenanceHistories)
                        .ThenInclude(s => s.ActionType)
                    .Where(e => e.ID == id)
                    .FirstOrDefaultAsync();
        }
    }
}
