using FaultTracker.Business.Interfaces;
using FaultTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FaultTracker.Business.Services
{
    public class PictureGroupService : CreatedRepositoryBase<PictureGroup>, IPictureGroupRepository
    {
        public PictureGroupService(DbContext context) : base(context)
        {
        }
    }
}
