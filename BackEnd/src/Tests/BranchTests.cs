namespace Tests
{
    using AutoMapper;
    using Domain.Dtos.Branch;
    using FluentValidation.TestHelper;
    using NSubstitute;
    using Services.MapperConfiguration;
    using Services.Validators.CommandValidators.Branch;
    using Services.Validators.QueryValidators;
    using Services.Validators.Shared;
    using System;
    using Tests.Utils;
    using Xunit;
    using static Services.CommandHandlers.BranchCommandHandlers;
    using static Services.QueryHandlers.BranchQueryHandlers;

    [Collection("Database collection")]
    public class BranchTests
    {
        private readonly InMemoryDbContextFixture _database;
        private readonly ICommonValidators _commonValidator;
        private readonly IMapper _mapper;
        public BranchTests(InMemoryDbContextFixture database)
        {
            _commonValidator = Substitute.For<ICommonValidators>();
            _database = database;
            _commonValidator.ConfigureTestData(_database.Context.DLO_Branches.ToList());
            _commonValidator.ConfigureTestData(_database.Context.DLO_Currencies.ToList());
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<BranchProfile>();
                opts.AddProfile<CurrencyProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Adding_Branch()
        {
            // Arrange

            var command = new AddBranchCommandValidator(_commonValidator);
            var request = new AddBranchCommand
            {
                Code = new Random().Next(),
                Address = "Cl",
                CreateDate = DateTime.Now,
                Description = "d1",
                Identification = "id1"
            };

            // Act
            var result = await command.TestValidateAsync(request);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task Update_Branch()
        {
            // Arrange

            var commandValidator = new UpdateBranchCommandValidator(_commonValidator);
            var _request = new UpdateBranchCommand
            {
                BranchId = 1,
                Code = 1001,
                Address = "Cl",
                CreateDate = DateTime.Now,
                Description = "d12a",
                Identification = "id1"
            };
            var previous = _database.Context.DLO_Branches.First(x => x.BranchId == 1).Description;

            // Act
            var result = await commandValidator.TestValidateAsync(_request);
            var handler = new UpdateBranchCommandHandler(_database.Context, _mapper);
            var response = await handler.Handle(_request, new CancellationToken());
            var actual = _database.Context.DLO_Branches.First(x => x.BranchId == 1).Description;

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
            Assert.NotNull(response.Result);
            Assert.NotEqual(actual, previous);
        }

        [Fact]
        public async Task Delete_Branch()
        {
            // Arrange

            var commandValidator = new DeleteBranchCommandValidator(_commonValidator);
            var _request = new DeleteBranchCommand
            {
                BranchId = 3
            };

            // Act
            var result = await commandValidator.TestValidateAsync(_request);
            var handler = new DeleteBranchCommandHandler(_database.Context, _mapper);
            var response = await handler.Handle(_request, new CancellationToken());

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
            Assert.NotNull(response.Result);
            Assert.Null(_database.Context.DLO_Branches.FirstOrDefault(x=>x.BranchId == 3));
        }

        [Fact]
        public async Task Get_Branch_By_Id()
        {
            // Arrange

            var validator = new GetBranchQueryValidator(_commonValidator);
            var _request = new GetBranchQuery
            {
                BranchId = 1
            };

            // Act
            var result = await validator.TestValidateAsync(_request);
            var handler = new GetBranchQueryHandler(_database.Context,_mapper);
            var response = await handler.Handle(_request, new CancellationToken());

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
            Assert.True(response.Result.Any());
        }

        [Fact]
        public async Task Get_Branches()
        {
            // Arrange

            var validator = new GetBranchQueryValidator(_commonValidator);
            var _request = new GetBranchQuery();

            // Act
            var result = await validator.TestValidateAsync(_request);
            var handler = new GetBranchQueryHandler(_database.Context, _mapper);
            var response = await handler.Handle(_request, new CancellationToken());

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
            Assert.True(response.Result.Any());
        }

    }
}
