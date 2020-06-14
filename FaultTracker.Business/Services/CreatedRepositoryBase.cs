using FaultTracker.Business.Interfaces;
using FaultTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaultTracker.Business.Services
{
    public class CreatedRepositoryBase<T> : Repository<T>, ICreatedRepositoryBase<T> where T : BaseEntityModel
    {
        public CreatedRepositoryBase(DbContext context) : base(context)
        {

        }

        /// <summary>
        /// Get a list of entity by filtering CreateDate property between two dates startDate and finishDate 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="finishDate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllByCreatedDateAsync(DateTime startDate, DateTime finishDate)
        {
            return await _entity
                    .Where(e =>
                        (startDate != null ? e.CreateDate >= startDate : true)
                        && (finishDate != null ? e.CreateDate <= finishDate : true))
                    .ToListAsync();
        }

        /// <summary>
        /// Get a list of entity that's created by only one user
        /// </summary>
        /// <param name="createdUserID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllByCreatedUserIdAsync(int createdUserID)
        {
            return await _entity
                     .Where(e => e.CreatedBy == createdUserID)
                     .ToListAsync();
        }

        /// <summary>
        /// Get a list of entity by filtering ModifiedDate property between two dates startDate and finishDate 
        /// <para>If one of them is not provided that's not gonna add in query</para>
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="finishDate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllByModifiedDateAsync(DateTime startDate, DateTime finishDate)
        {
            return await _entity
                    .Where(e =>
                        (startDate != null ? e.ModifyDate >= startDate : true)
                        && (finishDate != null ? e.ModifyDate <= finishDate : true))
                    .ToListAsync();
        }

        /// <summary>
        /// Get a list of entity that's updated by only one user
        /// <para>Exp: only for specific admin or user update</para>
        /// </summary>
        /// <param name="modifiedUserID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllByModifiedUserIdAsync(int modifiedUserID)
        {
            return await _entity
                .Where(e => e.ModifiedBy == modifiedUserID)
                .ToListAsync();
        }

        /// <summary>
        /// Get a list of entity that's IsDeleted = True
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllDeletedAsync()
        {
            return await _entity
                .Where(e =>
                    e.IsDeleted == true)
                .ToListAsync();
        }

        /// <summary>
        /// Get a list of entity that's IsDeleted = False
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllNonDeltedAsync()
        {
            return await _entity
                .Where(e =>
                    e.IsDeleted == false)
                .ToListAsync();
        }
    }
}
