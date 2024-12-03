using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sale.UpdateSale;

public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
{
    public UpdateSaleValidator()
    {
        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("Customer is required.")
            .WithMessage("Customer ID must be greater than 0");

        RuleFor(x => x.BranchId)
            .GreaterThan(0).WithMessage("Branch is required.")
            .WithMessage("Branch ID must be greater than 0");

        RuleFor(x => x.IsCancelled)
            .NotNull().WithMessage("Cancelled status is required.");
    }
}

public class CreateSaleItemValidator : AbstractValidator<UpdateSaleItemCommand>
{
    public CreateSaleItemValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage("Product name is required.")
            .Length(1, 200).WithMessage("Product name must be between 1 and 200 characters.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.")
            .LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 identical items.")
            .GreaterThanOrEqualTo(4).WithMessage("Quantity must be at least 4 for discount to apply.");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("Unit price must be greater than zero.")
            .ScalePrecision(2, 18).WithMessage("Unit price has invalid precision or scale.");
    }
}
