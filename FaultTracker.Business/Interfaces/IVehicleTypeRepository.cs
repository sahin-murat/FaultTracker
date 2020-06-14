using FaultTracker.Data.Entities;

namespace FaultTracker.Business.Interfaces
{
    public interface IVehicleTypeRepository : IRepository<VehicleType>, ICreatedRepositoryBase<VehicleType>
    {
    }
}
