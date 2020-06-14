using FaultTracker.Business.Interfaces;
using FaultTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public IEnumerable<T> GetAllByCreatedDate(DateTime startDate, DateTime finishDate)
        {
            return _entity
                    .Where(e =>
                        (startDate != null ? e.CreateDate >= startDate : true)
                        && (finishDate != null ? e.CreateDate <= finishDate : true))
                    .ToList();
        }

        /// <summary>
        /// Get a list of entity that's created by only one user
        /// </summary>
        /// <param name="createdUserID"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAllByCreatedUserId(int createdUserID)
        {
            return _entity
             .Where(e => e.CreatedBy == createdUserID)
             .ToList();
        }

        /// <summary>
        /// Get a list of entity by filtering ModifiedDate property between two dates startDate and finishDate 
        /// <para>If one of them is not provided that's not gonna add in query</para>
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="finishDate"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAllByModifiedDate(DateTime startDate, DateTime finishDate)
        {
            return _entity
                    .Where(e =>
                        (startDate != null ? e.ModifyDate >= startDate : true)
                        && (finishDate != null ? e.ModifyDate <= finishDate : true))
                    .ToList();
        }

        /// <summary>
        /// Get a list of entity that's updated by only one user
        /// <para>Exp: only for specific admin or user update</para>
        /// </summary>
        /// <param name="modifiedUserID"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAllByModifiedUserId(int modifiedUserID)
        {
            return _entity
                .Where(e => e.ModifiedBy == modifiedUserID)
                .ToList();
        }

        /// <summary>
        /// Get a list of entity that's IsDeleted = True
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAllDeleted()
        {
            return _entity
                .Where(e =>
                    e.IsDeleted == true)
                .ToList();
        }

        /// <summary>
        /// Get a list of entity that's IsDeleted = False
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAllNonDelted()
        {
            return _entity
                .Where(e =>
                    e.IsDeleted == false)
                .ToList();
        }
    }
}
