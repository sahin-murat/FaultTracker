using FaultTracker.Business.Interfaces;
using FaultTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FaultTracker.Business.Services
{
    public class UserService : CreatedRepositoryBase<User>, IUserRepository
    {
        public UserService(DbContext context) : base(context)
        {
        }
    }
}
