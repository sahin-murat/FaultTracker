using FaultTracker.Business.Interfaces;
using FaultTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FaultTracker.Business.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _entity;
        public Repository(DbContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }

        /// <summary>
        /// Add new Entity into DbContext
        /// </summary>
        /// <param name="entity">Send your entity class</param>
        public async void AddAsync(T entity)
        {
            await _context.AddAsync(entity);
        }

        /// <summary>
        /// Add some list of entities into DbContext
        /// </summary>
        /// <param name="entities">Send List with your entity class</param>
        public async void AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.AddRangeAsync(entities);
        }
        /// <summary>
        /// Updates the entity in context without committing
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            _entity.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Find the entities with expression / query
        /// </summary>
        /// <param name="predicate">Send expression clause (e => e.Property == something ) etc.</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entity.Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Get first record in Db, if empty null will be returned
        /// </summary>
        /// <returns></returns>
        public async Task<T> FirstAsync()
        {
            return await _entity.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get an entity record with it's Id
        /// </summary>
        /// <param name="id">Int Id</param>
        /// <returns></returns>
        public async Task<T> GetAsync(int id)
        {
            return await _entity.FindAsync(id);
        }

        /// <summary>
        /// Get all entity records without any filter
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entity.ToListAsync();
        }

        /// <summary>
        /// Get last record in db for your entity, if empty return will be null
        /// </summary>
        /// <returns></returns>
        public async Task<T> LastAsync()
        {
            return await _entity.LastOrDefaultAsync();
        }

        /// <summary>
        /// Remove an entity from DbContext
        /// </summary>
        /// <param name="entity">Send the entity class you wan't to delete</param>
        public void Remove(T entity)
        {
           _entity.Remove(entity);
        }

        /// <summary>
        /// Remove list of entity from DbContext
        /// </summary>
        /// <param name="entities">Send List of your entity to delete in a range</param>
        public void RemoveRange(IEnumerable<T> entities)
        {
            _entity.RemoveRange(entities);
        }
    }
}
