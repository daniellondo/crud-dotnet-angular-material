namespace Services.Validators.CommandValidators.Branch
{
    using Domain.Dtos.Branch;
    using Domain.Entities;
    using FluentValidation;
    using Services.Validators.Shared;

    public class DeleteBranchCommandValidator : AbstractValidator<DeleteBranchCommand>
    {
        public DeleteBranchCommandValidator(ICommonValidators commonValidators)
        {
            RuleFor(payload => payload.BranchId).NotNull();
            RuleFor(payload => payload.BranchId)
                .Cascade(CascadeMode.Stop)
                .Must(id => commonValidators
                .IsExistingEntityRow<BranchEntity>(t => t.BranchId == id))
                .WithMessage("BranchId must be valid.")
                .When(payload => payload != null);
        }
    }
}
