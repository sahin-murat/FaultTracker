using FaultTracker.Data.Entities;

namespace FaultTracker.Business.Interfaces
{
    public interface IVehicleRepository : IRepository<Vehicle>, ICreatedRepositoryBase<Vehicle>
    {
    }
}
