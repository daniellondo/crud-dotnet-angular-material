namespace Services.Validators.CommandValidators.Branch
{
    using Domain.Dtos.Branch;
    using Domain.Entities;
    using FluentValidation;
    using Services.Validators.Shared;

    public class UpdateBranchCommandValidator : AbstractValidator<UpdateBranchCommand>
    {
        public UpdateBranchCommandValidator(ICommonValidators commonValidators)
        {
            RuleFor(payload => payload.BranchId).NotEmpty();
            RuleFor(payload => payload.Address).NotEmpty().Length(1, 250);
            RuleFor(payload => payload.Code).NotEmpty();
            RuleFor(payload => payload.Description).NotEmpty().Length(1, 250);
            RuleFor(payload => payload.Identification).NotEmpty().Length(1, 50);
            RuleFor(payload => payload.CreateDate).NotEmpty().GreaterThanOrEqualTo(DateTime.Now);
            RuleFor(payload => payload.CurrencyId).NotEmpty();

            RuleFor(payload => payload.BranchId)
                .Cascade(CascadeMode.Stop)
                .Must(id => commonValidators
                .IsExistingEntityRow<BranchEntity>(t => t.BranchId == id))
                .WithMessage("BranchId must be valid.")
                .When(payload => payload != null);

            RuleFor(payload => payload.CurrencyId)
                    .Cascade(CascadeMode.Stop)
                    .Must(id => commonValidators
                    .IsExistingEntityRow<CurrencyEntity>(t => t.CurrencyId == id))
                    .WithMessage("CurrencyId must be valid.")
                    .When(payload => payload != null);


        }
    }
}
