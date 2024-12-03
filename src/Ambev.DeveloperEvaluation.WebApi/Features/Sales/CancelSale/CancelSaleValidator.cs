using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;

public class CancelSaleRequestValidator : AbstractValidator<CancelSaleRequest>
{
    public CancelSaleRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Sale ID is required.")
            .WithMessage("Sale ID must be greater than 0");
    }
}