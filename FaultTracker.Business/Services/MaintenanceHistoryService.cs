using FaultTracker.Business.Interfaces;
using FaultTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FaultTracker.Business.Services
{
    public class MaintenanceHistoryService : CreatedRepositoryBase<MaintenanceHistory>, IMaintenanceHistoryRepository
    {
        public MaintenanceHistoryService(DbContext context) : base(context)
        {
        }
    }
}
