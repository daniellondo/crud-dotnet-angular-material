namespace Tests
{
    using AutoMapper;
    using Domain.Dtos.Currency;
    using FluentValidation.TestHelper;
    using NSubstitute;
    using Services.MapperConfiguration;
    using Services.Validators.CommandValidators.Currency;
    using Services.Validators.QueryValidators;
    using Services.Validators.Shared;
    using System;
    using Tests.Utils;
    using Xunit;
    using static Services.CommandHandlers.CurrencyCommandHandlers;
    using static Services.QueryHandlers.CurrencyQueryHandlers;

    [Collection("Database collection")]
    public class CurrencyTests
    {
        private readonly InMemoryDbContextFixture _database;
        private readonly ICommonValidators _commonValidator;
        private readonly IMapper _mapper;
        public CurrencyTests(InMemoryDbContextFixture database)
        {
            _commonValidator = Substitute.For<ICommonValidators>();
            _database = database;
            _commonValidator.ConfigureTestData(_database.Context.DLO_Currencies.ToList());
            _commonValidator.ConfigureTestData(_database.Context.DLO_Currencies.ToList());
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<CurrencyProfile>();
                opts.AddProfile<CurrencyProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Adding_Currency()
        {
            // Arrange

            var command = new AddCurrencyCommandValidator(_commonValidator);
            var request = new AddCurrencyCommand
            {
                Currency = "Peso Colombiano",
                RefCode = "COP",
                CreateDate = DateTime.UtcNow,
            };

            // Act
            var result = await command.TestValidateAsync(request);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task Update_Currency()
        {
            // Arrange

            var commandValidator = new UpdateCurrencyCommandValidator(_commonValidator);
            var _request = new UpdateCurrencyCommand
            {
                CurrencyId = 2,
                Currency = "Peso Chileno",
                RefCode = "CLP",
                CreateDate = DateTime.UtcNow,
            };
            var previous = _database.Context.DLO_Currencies.First(x => x.CurrencyId == 2).RefCode;

            // Act
            var result = await commandValidator.TestValidateAsync(_request);
            var handler = new UpdateCurrencyCommandHandler(_database.Context, _mapper);
            var response = await handler.Handle(_request, new CancellationToken());
            var actual = _database.Context.DLO_Currencies.First(x => x.CurrencyId == 2).RefCode;

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
            Assert.True(response.Result);
            Assert.NotEqual(actual, previous);
        }

        [Fact]
        public async Task Delete_Currency()
        {
            // Arrange

            var commandValidator = new DeleteCurrencyCommandValidator(_commonValidator);
            var _request = new DeleteCurrencyCommand
            {
                CurrencyId = 2
            };

            // Act
            var result = await commandValidator.TestValidateAsync(_request);
            var handler = new DeleteCurrencyCommandHandler(_database.Context, _mapper);
            var response = await handler.Handle(_request, new CancellationToken());

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
            Assert.NotNull(response.Result);
            Assert.Null(_database.Context.DLO_Currencies.FirstOrDefault(x=>x.CurrencyId == 2));
        }

        [Fact]
        public async Task Get_Currency_By_Id()
        {
            // Arrange

            var validator = new GetCurrencyQueryValidator(_commonValidator);
            var _request = new GetCurrencyQuery
            {
                CurrencyId = 2
            };

            // Act
            var currencies = _database.Context.DLO_Currencies.ToList();
            var result = await validator.TestValidateAsync(_request);
            var handler = new GetCurrencyQueryHandler(_database.Context,_mapper);
            var response = await handler.Handle(_request, new CancellationToken());

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
            Assert.True(response.Result.Any());
        }

        [Fact]
        public async Task Get_Currencies()
        {
            // Arrange

            var validator = new GetCurrencyQueryValidator(_commonValidator);
            var _request = new GetCurrencyQuery();

            // Act
            var result = await validator.TestValidateAsync(_request);
            var handler = new GetCurrencyQueryHandler(_database.Context, _mapper);
            var response = await handler.Handle(_request, new CancellationToken());

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
            Assert.True(response.Result.Any());
        }

    }
}
