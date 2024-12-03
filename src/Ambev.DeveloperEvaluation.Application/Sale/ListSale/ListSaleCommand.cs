using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.ListSale;

public record ListSaleCommand : IRequest<ListSaleResult>
{
    public ListSaleCommand()
    {

    }
}
