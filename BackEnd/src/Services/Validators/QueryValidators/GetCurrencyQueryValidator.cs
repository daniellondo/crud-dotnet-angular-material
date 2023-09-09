namespace Services.Validators.QueryValidators
{
    using Domain.Dtos.Currency;
    using Domain.Entities;
    using FluentValidation;
    using Services.Validators.Shared;

    public class GetCurrencyQueryValidator : AbstractValidator<GetCurrencyQuery>
    {
        public GetCurrencyQueryValidator(ICommonValidators commonValidators)
        {
            RuleFor(payload => payload.CurrencyId)
                .Cascade(CascadeMode.Stop)
                .Must(id => commonValidators
                .IsExistingEntityRow<CurrencyEntity>(t => t.CurrencyId == id))
                .WithMessage("CurrencyId must be valid.")
                .When(payload => payload != null && payload.CurrencyId is not null);
        }
    }
}
