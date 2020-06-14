using System;
using System.Collections.Generic;

namespace FaultTracker.Business.Interfaces
{
    public interface ICreatedRepositoryBase<T> where T : class
    {
        /// <summary>
        /// Get a list of entity that's IsDeleted = False
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAllNonDelted();

        /// <summary>
        /// Get a list of entity that's IsDeleted = True
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAllDeleted();

        /// <summary>
        /// Get a list of entity by filtering CreateDate property between two dates startDate and finishDate 
        /// </summary>
        /// <param name="startDate">StartDate as Datetime</param>
        /// <param name="finishDate">FinishDate as Datetime</param>
        /// <returns></returns>
        IEnumerable<T> GetAllByCreatedDate(DateTime startDate, DateTime finishDate);

        /// <summary>
        ///  Get a list of entity by filtering ModifiedDate property between two dates startDate and finishDate 
        /// <para>If one of them is not provided that's not gonna add in query</para>
        /// </summary>
        /// <param name="startDate">StartDate as Datetime</param>
        /// <param name="finishDate">FinishDate as Datetime</param>
        IEnumerable<T> GetAllByModifiedDate(DateTime startDate, DateTime finishDate);

        /// <summary>
        ///  Get a list of entity that's created by only one user
        /// </summary>
        /// <param name="createdUserID">Created User Id</param>
        /// <returns></returns>
        IEnumerable<T> GetAllByCreatedUserId(int createdUserID);

        /// <summary>
        /// Get a list of entity that's updated by only one user
        /// <para>Exp: only for specific admin or user update</para>
        /// </summary>
        /// <param name="modifiedUserID">Updated User Id</param>
        /// <returns></returns>
        IEnumerable<T> GetAllByModifiedUserId(int modifiedUserID);

    }
}
