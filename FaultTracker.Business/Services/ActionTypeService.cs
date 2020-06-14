using FaultTracker.Business.Interfaces;
using FaultTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FaultTracker.Business.Services
{
    public class ActionTypeService : CreatedRepositoryBase<ActionType>, IActionTypeRepository
    {
        public ActionTypeService(DbContext context) : base(context)
        {
        }
    }
}
