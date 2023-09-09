namespace Services.Validators.QueryValidators
{
    using Domain.Dtos.Branch;
    using Domain.Entities;
    using FluentValidation;
    using Services.Validators.Shared;

    public class GetBranchQueryValidator : AbstractValidator<GetBranchQuery>
    {
        public GetBranchQueryValidator(ICommonValidators commonValidators)
        {
            RuleFor(payload => payload.BranchId)
                .Cascade(CascadeMode.Stop)
                .Must(id => commonValidators
                .IsExistingEntityRow<BranchEntity>(t => t.BranchId == id))
                .WithMessage("BranchId must be valid.")
                .When(payload => payload != null && payload.BranchId is not null);
        }
    }
}
