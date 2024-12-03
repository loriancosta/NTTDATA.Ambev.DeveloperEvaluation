namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequest
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int BranchId { get; set; }
    public bool IsCancelled { get; set; } = false;
    public List<CreateSaleItemRequest> Items { get; set; } = new List<CreateSaleItemRequest>();
}

public class CreateSaleItemRequest
{
    public int SaleId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}