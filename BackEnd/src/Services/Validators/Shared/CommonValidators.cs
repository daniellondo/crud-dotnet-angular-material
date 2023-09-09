
namespace Services.Validators.Shared
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class CommonValidators : ICommonValidators
    {
        private readonly IContext _context;

        public CommonValidators(IContext context)
        {
            _context = context;
        }

        public bool IsExistingEntityRow<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            var query = _context.Set<TEntity>();
            return query.Any(filter);
        }

        public async Task<TEntity> GetFirstOrDefaultEntityRowAsync<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            var query = _context.Set<TEntity>();
            return await query.FirstOrDefaultAsync(filter);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}
