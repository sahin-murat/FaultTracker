using FaultTracker.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FaultTracker.Business.Interfaces
{
    public interface IMaintenanceHistoryRepository : IRepository<MaintenanceHistory>, ICreatedRepositoryBase<MaintenanceHistory>
    {
        Task<MaintenanceHistory> GetWithAllRelationsAsync(int id);

        Task<IEnumerable<MaintenanceHistory>> GetAllWithAllRelationsAsync();
    }
}
