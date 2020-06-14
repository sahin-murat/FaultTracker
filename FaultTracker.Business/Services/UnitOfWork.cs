using FaultTracker.Business.Interfaces;
using FaultTracker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

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
        public async Task<bool> BeginNewTransactionAsync()
        {
            try
            {
                _transaction = await _context.Database.BeginTransactionAsync();
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
        public async Task<int> CompleteAsync()
        {
            var transaction = _transaction != null ? _transaction : await _context.Database.BeginTransactionAsync();

            using (transaction)
            {
                try
                {
                    if (_context is null)
                    {
                        throw new ArgumentException("Context is null");
                    }

                    int result = await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return result;
                }
                catch (Exception ex)
                {
                     await transaction.RollbackAsync();
                    throw new Exception("Error on complete", ex);
                }
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<bool> RollBackTransactionAsync()
        {
            try
            {
                 await _transaction.RollbackAsync();
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
