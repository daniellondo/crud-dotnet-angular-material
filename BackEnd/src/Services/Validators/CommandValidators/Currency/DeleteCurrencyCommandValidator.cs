namespace Services.Validators.CommandValidators.Currency
{
    using Domain.Dtos.Currency;
    using Domain.Entities;
    using FluentValidation;
    using Services.Validators.Shared;

    public class DeleteCurrencyCommandValidator : AbstractValidator<DeleteCurrencyCommand>
    {
        public DeleteCurrencyCommandValidator(ICommonValidators commonValidators)
        {
            RuleFor(payload => payload.CurrencyId).NotNull();
            RuleFor(payload => payload.CurrencyId)
                .Cascade(CascadeMode.Stop)
                .Must(id => commonValidators
                .IsExistingEntityRow<CurrencyEntity>(t => t.CurrencyId == id))
                .WithMessage("CurrencyId must be valid.")
                .When(payload => payload != null);
        }
    }
}
