namespace Data
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    public interface IContext : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        public DbSet<BranchEntity> DLO_Branches { get; set; }
        public DbSet<CurrencyEntity> DLO_Currencies { get; set; }

    }
}
