using FaultTracker.Business.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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
        public void Add(T entity)
        {
            _context.Add(entity);
        }

        /// <summary>
        /// Add some list of entities into DbContext
        /// </summary>
        /// <param name="entities">Send List with your entity class</param>
        public void AddRange(IEnumerable<T> entities)
        {
            _context.AddRange(entities);
        }

        /// <summary>
        /// Find the entities with expression / query
        /// </summary>
        /// <param name="predicate">Send expression clause (e => e.Property == something ) etc.</param>
        /// <returns></returns>
        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            return _entity.Where(predicate).ToList();
        }

        /// <summary>
        /// Get first record in Db, if empty null will be returned
        /// </summary>
        /// <returns></returns>
        public T First()
        {
            return _entity.FirstOrDefault();
        }

        /// <summary>
        /// Get an entity record with it's Id
        /// </summary>
        /// <param name="id">Int Id</param>
        /// <returns></returns>
        public T Get(int id)
        {
            return _entity.Find(id);
        }

        /// <summary>
        /// Get all entity records without any filter
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            return _entity.ToList();
        }

        /// <summary>
        /// Get last record in db for your entity, if empty return will be null
        /// </summary>
        /// <returns></returns>
        public T Last()
        {
            return _entity.LastOrDefault();
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
