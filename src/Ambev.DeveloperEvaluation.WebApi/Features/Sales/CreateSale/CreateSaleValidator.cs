using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleValidator()
    {
        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("Customer is required.")
            .WithMessage("Customer ID must be greater than 0");

        RuleFor(x => x.BranchId)
            .GreaterThan(0).WithMessage("Branch is required.")
            .WithMessage("Branch ID must be greater than 0");

        RuleFor(x => x.IsCancelled)
            .NotNull().WithMessage("Cancelled status is required.");

        RuleForEach(x => x.Items).SetValidator(new CreateSaleItemValidator());
    }
}

public class CreateSaleItemValidator : AbstractValidator<CreateSaleItemRequest>
{
    public CreateSaleItemValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage("Product name is required.")
            .Length(1, 200).WithMessage("Product name must be between 1 and 200 characters.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.")
            .LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 identical items.");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("Unit price must be greater than zero.")
            .ScalePrecision(2, 18).WithMessage("Unit price has invalid precision or scale.");

    }
}
