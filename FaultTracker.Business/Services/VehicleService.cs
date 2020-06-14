using FaultTracker.Business.Interfaces;
using FaultTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FaultTracker.Business.Services
{
    public class VehicleService : CreatedRepositoryBase<Vehicle>, IVehicleRepository
    {
        public VehicleService(DbContext context) : base(context)
        {
        }
    }
}
