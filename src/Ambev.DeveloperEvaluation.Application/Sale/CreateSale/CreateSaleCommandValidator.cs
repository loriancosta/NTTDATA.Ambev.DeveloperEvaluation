using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("Customer is required.")
            .WithMessage("Customer name must be between 1 and 100 characters.");

        RuleFor(x => x.BranchId)
            .NotEmpty().WithMessage("Branch is required.")
            .WithMessage("Branch name must be between 1 and 50 characters.");

    }
}
