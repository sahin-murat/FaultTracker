using FaultTracker.Data.Entities;

namespace FaultTracker.Business.Interfaces
{
    public interface IUserRepository : IRepository<User>, ICreatedRepositoryBase<User>
    {

    }
}
