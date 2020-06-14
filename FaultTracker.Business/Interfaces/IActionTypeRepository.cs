using FaultTracker.Data.Entities;

namespace FaultTracker.Business.Interfaces
{
    public interface IActionTypeRepository : IRepository<ActionType>, ICreatedRepositoryBase<ActionType>
    {
    }
}
