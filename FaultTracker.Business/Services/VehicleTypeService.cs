using FaultTracker.Business.Interfaces;
using FaultTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FaultTracker.Business.Services
{
    public class VehicleTypeService : CreatedRepositoryBase<VehicleType>, IVehicleTypeRepository
    {
        public VehicleTypeService(DbContext context) : base(context)
        {
        }
    }
}
