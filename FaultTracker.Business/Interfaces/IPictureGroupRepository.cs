using FaultTracker.Data.Entities;

namespace FaultTracker.Business.Interfaces
{
    public interface IPictureGroupRepository : IRepository<PictureGroup>, ICreatedRepositoryBase<PictureGroup>
    {
    }
}
