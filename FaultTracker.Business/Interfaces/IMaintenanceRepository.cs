using FaultTracker.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FaultTracker.Business.Interfaces
{
    public interface IMaintenanceRepository : IRepository<Maintenance>, ICreatedRepositoryBase<Maintenance>
    {
        /// <summary>
        /// Get Maintenance details with all the relations
        /// <para>Includes: Vehicle, User, Status, PictureGroup, MaintenanceHistories</para>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Maintenance> GetWithAllRelationsAsync(int id);

        /// <summary>
        /// Get AllMaintenance details with all the relations
        /// <para>Includes: Vehicle, User, Status, PictureGroup, MaintenanceHistories</para>
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Maintenance>> GetAllWithAllRelationsAsync();
    }
}
