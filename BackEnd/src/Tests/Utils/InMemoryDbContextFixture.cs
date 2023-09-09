namespace Tests.Utils
{
    using AutoFixture;
    using Data;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using NSubstitute;
    using System;

    public class InMemoryDbContextFixture : IDisposable
    {
        public IContext Context { get; private set; }

        public readonly IContext _mockContext;
        public static readonly Fixture _fixture = new();

        public InMemoryDbContextFixture()
        {
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            Context = GetInMemoryDbContext();
            _mockContext = Substitute.For<IContext>();
            InitializeData(Context, _mockContext);
        }

        private void InitializeData(IContext context, IContext mockContext)
        {
            context.DLO_Branches.AddRange(GetBranchesArranged());
            context.DLO_Currencies.AddRange(GetCurrenciesArranged());
            context.SaveChanges();

            mockContext.DLO_Branches.Returns(context.DLO_Branches);
            mockContext.DLO_Currencies.Returns(context.DLO_Currencies);
        }

        private List<CurrencyEntity> GetCurrenciesArranged()
        {
            return new List<CurrencyEntity> {
                _fixture.Build<CurrencyEntity>()
                    .With(p => p.CurrencyId, 1)
                    .Create(),
                _fixture.Build<CurrencyEntity>()
                    .With(p => p.CurrencyId, 2)
                    .Create(),
                _fixture.Build<CurrencyEntity>()
                    .With(p => p.CurrencyId, 3)
                    .Create(),
                _fixture.Build<CurrencyEntity>()
                    .Create(),
                _fixture.Build<CurrencyEntity>()
                    .Create(),
                _fixture.Build<CurrencyEntity>()
                    .With(p => p.CurrencyId, 5)
                    .Create()
            };
        }

        private List<BranchEntity> GetBranchesArranged()
        {
            return new List<BranchEntity> {
                _fixture.Build<BranchEntity>()
                    .With(p => p.BranchId, 1)
                    .Without(p => p.Currency)
                    .Create(),
                _fixture.Build<BranchEntity>()
                    .With(p => p.BranchId, 2)
                    .Without(p => p.Currency)
                    .Create(),
                _fixture.Build<BranchEntity>()
                    .With(p => p.BranchId, 3)
                    .Without(p => p.Currency)
                    .Create(),
            };
        }

        public static Context GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<Context>()
              .UseInMemoryDatabase(Guid.NewGuid().ToString())
              .LogTo(Console.WriteLine)
              .EnableDetailedErrors(true)
              .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
              .EnableSensitiveDataLogging(true)
              .Options;

            return new Context(options);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Context.Dispose();
        }

        ~InMemoryDbContextFixture()
        {
            Dispose(false);
        }

    }
}
