using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace FarmFresh.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
        Task SaveChangesAsync();
        public Task<IDbContextTransaction> BeginTransaction();
    }
}
