namespace Services.Validators.CommandValidators.Branch
{
    using Domain.Dtos.Branch;
    using Domain.Entities;
    using FluentValidation;
    using Services.Validators.Shared;

    public class AddBranchCommandValidator : AbstractValidator<AddBranchCommand>
    {
        public AddBranchCommandValidator(ICommonValidators commonValidators)
        {
            RuleFor(payload => payload.Code)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .Must(code => !commonValidators.IsExistingEntityRow<BranchEntity>(x => x.Code == code))
                .WithMessage("You cannot add 2 branches with the same code");

            RuleFor(payload => payload.Address).NotEmpty().Length(1, 250);
            RuleFor(payload => payload.Code).NotEmpty();
            RuleFor(payload => payload.Description).NotEmpty().Length(1, 250);
            RuleFor(payload => payload.Identification).NotEmpty().Length(1, 50); 
            RuleFor(payload => payload.CreateDate).NotEmpty().GreaterThanOrEqualTo(DateTime.Now);
        }
    }
}
