using FaultTracker.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FaultTracker.Business.Interfaces
{
    public interface IVehicleRepository : IRepository<Vehicle>, ICreatedRepositoryBase<Vehicle>
    {
        Task<Vehicle> GetVehicleWithRelationsAsync(int id);
        Task<IEnumerable<Vehicle>> GetAllVehiclesWithRelationsAsync();
    }
}
