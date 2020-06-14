using FaultTracker.Data.Entities;

namespace FaultTracker.Business.Interfaces
{
    public interface IMaintenanceRepository : IRepository<Maintenance>, ICreatedRepositoryBase<Maintenance>
    {
    }
}
