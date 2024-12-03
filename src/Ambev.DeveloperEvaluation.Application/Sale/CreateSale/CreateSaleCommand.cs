using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale;

public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    public int CustomerId { get; set; }
    public int BranchId { get; set; }
    public bool IsCancelled { get; set; } = false;
    public List<CreateSaleItemCommand> Items { get; set; } = new List<CreateSaleItemCommand>();
}

public class CreateSaleItemCommand
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}