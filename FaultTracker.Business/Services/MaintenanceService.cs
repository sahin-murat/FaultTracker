using FaultTracker.Business.Interfaces;
using FaultTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FaultTracker.Business.Services
{
    public class MaintenanceService : CreatedRepositoryBase<Maintenance>, IMaintenanceRepository
    {
        public MaintenanceService(DbContext context) : base(context)
        {
        }
    }
}
