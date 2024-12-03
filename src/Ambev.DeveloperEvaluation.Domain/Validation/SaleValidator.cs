using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        //RuleFor(x => x.CustomerId)
        //    .NotEmpty().WithMessage("Customer is required.")
        //    .Length(1, 100).WithMessage("Customer name must be between 1 and 100 characters.");

        //RuleFor(x => x.BranchId)
        //    .NotEmpty().WithMessage("Branch is required.")
        //    .Length(1, 50).WithMessage("Branch name must be between 1 and 50 characters.");

        //RuleFor(x => x.SaleItems)
        //    .NotEmpty().WithMessage("At least one item must be included in the sale.")
        //    .Must(items => items.Count() > 0).WithMessage("Items list must contain at least one item.")
        //    .ForEach(item => item.SetValidator(new SaleItemValidator()));  // Improved structure

        //RuleFor(x => x.SaleItems)
        //    .Must(HasValidDiscount).WithMessage("Discounts are not applicable for less than 4 items.");
    }

    private bool HasValidDiscount(List<SaleItem> items)
    {
        foreach (var item in items)
        {
            if (item.Quantity < 4)
            {
                return true;
            }

            if (item.Quantity > 20)
            {
                return false;
            }
        }
        return true;
    }
}

public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        //RuleFor(x => x.ProductName)
        //    .NotEmpty().WithMessage("Product name is required.")
        //    .Length(1, 200).WithMessage("Product name must be between 1 and 200 characters.");

        //RuleFor(x => x.Quantity)
        //    .GreaterThan(0).WithMessage("Quantity must be greater than zero.")
        //    .LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 identical items.");

        //RuleFor(x => x.UnitPrice)
        //    .GreaterThan(0).WithMessage("Unit price must be greater than zero.")
        //    .ScalePrecision(2, 18).WithMessage("Unit price has invalid precision or scale.");
    }
}
