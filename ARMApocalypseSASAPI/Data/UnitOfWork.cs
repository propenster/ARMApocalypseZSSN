using ARMApocalypseSASAPI.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace ARMApocalypseSASAPI.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            SurvivorRepository = new GenericRepository<Survivor>(_context);
            ItemRepository = new GenericRepository<Item>(_context);
            TradeItemRepository = new GenericRepository<TradeItem>(_context);
            SurvivorInfectionReportRepository = new GenericRepository<SurvivorInfectionReport>(_context);



        }
        //add all Repositories here! 
        public IGenericRepository<Survivor> SurvivorRepository { get; }
        public IGenericRepository<Item> ItemRepository { get; }

        public IGenericRepository<TradeItem> TradeItemRepository { get; }

        public IGenericRepository<SurvivorInfectionReport> SurvivorInfectionReportRepository { get; }

        IDbContextTransaction _objTransaction;

        public void Commit()
        {
            _objTransaction.Commit();
        }

        public void CreateTransaction()
        {
            _objTransaction = _context.Database.BeginTransaction();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public void Rollback()
        {
            _objTransaction.Rollback();
            _objTransaction.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}

