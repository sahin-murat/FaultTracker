using FaultTracker.Business.Interfaces;
using FaultTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FaultTracker.Business.Services
{
    public class StatusService : CreatedRepositoryBase<Status>, IStatusRepository
    {
        public StatusService(DbContext context) : base(context)
        {
        }
    }
}
