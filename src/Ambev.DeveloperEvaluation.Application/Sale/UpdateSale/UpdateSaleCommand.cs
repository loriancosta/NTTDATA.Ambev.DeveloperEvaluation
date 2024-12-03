using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.UpdateSale;

public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int BranchId { get; set; }
    public bool IsCancelled { get; set; } = false;
    public List<UpdateSaleItemCommand> Items { get; set; } = new List<UpdateSaleItemCommand>();
}

public class UpdateSaleItemCommand
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}