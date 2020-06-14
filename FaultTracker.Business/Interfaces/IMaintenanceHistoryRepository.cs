using FaultTracker.Data.Entities;

namespace FaultTracker.Business.Interfaces
{
    public interface IMaintenanceHistoryRepository : IRepository<MaintenanceHistory>, ICreatedRepositoryBase<MaintenanceHistory>
    {
    }
}
