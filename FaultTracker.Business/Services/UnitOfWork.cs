using FaultTracker.Business.Interfaces;
using FaultTracker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace FaultTracker.Business.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
            VehicleTypes = new VehicleTypeService(_context);
            Vehicles = new VehicleService(_context);
            Users = new UserService(_context);
            Statuses = new StatusService(_context);
            PictureGroups = new PictureGroupService(_context);
            MaintenanceHistories = new MaintenanceHistoryService(_context);
            Maintenances = new MaintenanceService(_context);
            ActionTypes = new ActionTypeService(_context);
        }


        public IVehicleTypeRepository VehicleTypes { get; private set; }

        public IVehicleRepository Vehicles { get; private set; }

        public IUserRepository Users { get; private set; }

        public IStatusRepository Statuses { get; private set; }

        public IPictureGroupRepository PictureGroups { get; private set; }

        public IMaintenanceHistoryRepository MaintenanceHistories { get; private set; }

        public IMaintenanceRepository Maintenances { get; private set; }

        public IActionTypeRepository ActionTypes { get; private set; }

        /// <summary>
        /// Create new transaction 
        /// </summary>
        /// <returns></returns>
        public bool BeginNewTransaction()
        {
            try
            {
                _transaction = _context.Database.BeginTransaction();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Apply all the CRUD operations in one transaction, if any error accured in applied operations DB will automatically Rollbacked
        /// </summary>
        /// <returns></returns>
        public int Complete()
        {
            var transaction = _transaction != null ? _transaction : _context.Database.BeginTransaction();

            using (transaction)
            {
                try
                {
                    if (_context is null)
                    {
                        throw new ArgumentException("Context is null");
                    }

                    int result = _context.SaveChanges();

                    transaction.Commit();

                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error on complete", ex);
                }
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public bool RollBackTransaction()
        {
            try
            {
                _transaction.Rollback();
                _transaction = null;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
