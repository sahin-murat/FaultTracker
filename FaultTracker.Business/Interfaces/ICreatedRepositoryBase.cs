using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FaultTracker.Business.Interfaces
{
    public interface ICreatedRepositoryBase<T> where T : class
    {
        /// <summary>
        /// Get a list of entity that's IsDeleted = False
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllNonDeltedAsync();

        /// <summary>
        /// Get a list of entity that's IsDeleted = True
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllDeletedAsync();

        /// <summary>
        /// Get a list of entity by filtering CreateDate property between two dates startDate and finishDate 
        /// </summary>
        /// <param name="startDate">StartDate as Datetime</param>
        /// <param name="finishDate">FinishDate as Datetime</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllByCreatedDateAsync(DateTime startDate, DateTime finishDate);

        /// <summary>
        ///  Get a list of entity by filtering ModifiedDate property between two dates startDate and finishDate 
        /// <para>If one of them is not provided that's not gonna add in query</para>
        /// </summary>
        /// <param name="startDate">StartDate as Datetime</param>
        /// <param name="finishDate">FinishDate as Datetime</param>
        Task<IEnumerable<T>> GetAllByModifiedDateAsync(DateTime startDate, DateTime finishDate);

        /// <summary>
        ///  Get a list of entity that's created by only one user
        /// </summary>
        /// <param name="createdUserID">Created User Id</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllByCreatedUserIdAsync(int createdUserID);

        /// <summary>
        /// Get a list of entity that's updated by only one user
        /// <para>Exp: only for specific admin or user update</para>
        /// </summary>
        /// <param name="modifiedUserID">Updated User Id</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllByModifiedUserIdAsync(int modifiedUserID);

    }
}
