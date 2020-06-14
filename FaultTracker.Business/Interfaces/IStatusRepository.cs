using FaultTracker.Data.Entities;

namespace FaultTracker.Business.Interfaces
{
    public interface IStatusRepository : IRepository<Status>, ICreatedRepositoryBase<Status>
    {
    }
}
