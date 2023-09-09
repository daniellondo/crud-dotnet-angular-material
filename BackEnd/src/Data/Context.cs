namespace Data
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    public class Context : DbContext, IContext
    {
        public DbSet<BranchEntity> DLO_Branches { get; set; }
        public DbSet<CurrencyEntity> DLO_Currencies { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var applyConfigurationMethodInfo = modelBuilder
                .GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .First(m => m.Name.Equals("ApplyConfiguration", StringComparison.OrdinalIgnoreCase));

            _ = typeof(Context).Assembly
                .GetTypes()
                .Select(t => (t,
                    i: t.GetInterfaces().FirstOrDefault(i =>
                        i.Name.Equals(typeof(IEntityTypeConfiguration<>).Name, StringComparison.Ordinal))))
                .Where(it => it.i != null)
                .Select(it => (et: it.i.GetGenericArguments()[0], cfgObj: Activator.CreateInstance(it.t)))
                .Select(it =>
                    applyConfigurationMethodInfo.MakeGenericMethod(it.et).Invoke(modelBuilder, new[] { it.cfgObj }))
                .ToList();

            base.OnModelCreating(modelBuilder);
        }
    }
}