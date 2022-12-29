using ARMApocalypseSASAPI.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ARMApocalypseSASAPI.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Survivor> SurvivorRepository { get; } //Replace this with yours e.g typeof(LoanDetail)
        IGenericRepository<Item> ItemRepository { get; }    
        IGenericRepository<TradeItem> TradeItemRepository { get; }    
        IGenericRepository<SurvivorInfectionReport> SurvivorInfectionReportRepository { get; }    
        void Commit();
        Task<int> SaveChangesAsync();
        void CreateTransaction();
        void Rollback();
    }
}

