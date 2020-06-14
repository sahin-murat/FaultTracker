using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FaultTracker.Business.Interfaces
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Get an entity record with it's Id
        /// </summary>
        /// <param name="id">Int Id</param>
        /// <returns></returns>
        Task<T> GetAsync(int id);

        /// <summary>
        /// Get first record in Db, if empty null will be returned
        /// </summary>
        /// <returns></returns>
        Task<T> FirstAsync();

        /// <summary>
        /// Get last record in db for your entity, if empty return will be null
        /// </summary>
        /// <returns></returns>
        Task<T> LastAsync();

        /// <summary>
        /// Get all entity records without any filter
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Find the entities with expression / query
        /// </summary>
        /// <param name="predicate">Send expression clause (e => e.Property == something ) etc.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Add new Entity into DbContext
        /// </summary>
        /// <param name="entity">Send your entity class</param>
        void AddAsync(T entity);

        /// <summary>
        /// Add some list of entities into DbContext
        /// </summary>
        /// <param name="entities">Send List with your entity class</param>
        void AddRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Remove an entity from DbContext
        /// </summary>
        /// <param name="entity">Send the entity class you wan't to delete</param>
        void Remove(T entity);

        /// <summary>
        /// Remove list of entity from DbContext
        /// </summary>
        /// <param name="entities">Send List of your entity to delete in a range</param>
        void RemoveRange(IEnumerable<T> entities);
    }
}
