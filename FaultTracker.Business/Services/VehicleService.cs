using FaultTracker.Business.Interfaces;
using FaultTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaultTracker.Business.Services
{
    public class VehicleService : CreatedRepositoryBase<Vehicle>, IVehicleRepository
    {
        public VehicleService(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehiclesWithRelationsAsync()
        {
            return await _entity.Include(r => r.User).Include(r => r.VehicleType).ToListAsync();
        }

        public async Task<Vehicle> GetVehicleWithRelationsAsync(int id)
        {
            return await _entity.Include(r => r.User).Include(r => r.VehicleType).Where(v => v.ID == id).FirstOrDefaultAsync();
        }
    }
}
