namespace Services.Validators.CommandValidators.Currency
{
    using Domain.Dtos.Currency;
    using Domain.Entities;
    using FluentValidation;
    using Services.Validators.Shared;

    public class UpdateCurrencyCommandValidator : AbstractValidator<UpdateCurrencyCommand>
    {
        public UpdateCurrencyCommandValidator(ICommonValidators commonValidators)
        {
            RuleFor(payload => payload.RefCode).NotEmpty();
            RuleFor(payload => payload.Currency).NotEmpty();

            RuleFor(payload => payload.CurrencyId)
                .Cascade(CascadeMode.Stop)
                .Must(id => commonValidators
                .IsExistingEntityRow<CurrencyEntity>(t => t.CurrencyId == id))
                .WithMessage("CurrencyId must be valid.")
                .When(payload => payload != null);


        }
    }
}
