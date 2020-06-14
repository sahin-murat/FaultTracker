using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FaultTracker.Business.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IVehicleTypeRepository VehicleTypes { get; }
        IVehicleRepository Vehicles { get; }
        IUserRepository Users { get; }
        IStatusRepository Statuses { get; }
        IPictureGroupRepository PictureGroups { get; }
        IMaintenanceHistoryRepository MaintenanceHistories { get; }
        IMaintenanceRepository Maintenances { get; }
        IActionTypeRepository ActionTypes { get;  }

        Task<bool> BeginNewTransactionAsync();
        Task<bool> RollBackTransactionAsync();

        Task<int> CompleteAsync();
    }
}
