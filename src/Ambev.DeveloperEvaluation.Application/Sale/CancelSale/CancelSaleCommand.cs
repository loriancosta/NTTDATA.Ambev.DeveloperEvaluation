using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.CancelSale;

public record CancelSaleCommand : IRequest<CancelSaleResult>
{
    public int Id { get; }

    public CancelSaleCommand(int id)
    {
        Id = id;
    }
}
