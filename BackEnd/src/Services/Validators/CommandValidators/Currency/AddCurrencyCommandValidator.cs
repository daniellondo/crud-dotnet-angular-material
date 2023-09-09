namespace Services.Validators.CommandValidators.Currency
{
    using Domain.Dtos.Currency;
    using Domain.Entities;
    using FluentValidation;
    using Services.Validators.Shared;

    public class AddCurrencyCommandValidator : AbstractValidator<AddCurrencyCommand>
    {
        public AddCurrencyCommandValidator(ICommonValidators commonValidators)
        {
            RuleFor(payload => payload.Currency)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .Must(currency => !commonValidators.IsExistingEntityRow<CurrencyEntity>(x => x.Currency == currency))
                .WithMessage("You cannot add 2 currency with the same code");

            RuleFor(payload => payload.RefCode).NotEmpty();
            RuleFor(payload => payload.Currency).NotEmpty();
        }
    }
}
