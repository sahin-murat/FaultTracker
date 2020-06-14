using System;
using System.Collections.Generic;
using System.Text;

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

        bool BeginNewTransaction();
        bool RollBackTransaction();

        int Complete();
    }
}
