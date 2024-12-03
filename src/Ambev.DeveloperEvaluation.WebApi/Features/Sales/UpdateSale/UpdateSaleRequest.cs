namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleRequest
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int BranchId { get; set; }
    public bool IsCancelled { get; set; } = false;
    public List<UpdateSaleItemRequest> Items { get; set; } = new List<UpdateSaleItemRequest>();
}

public class UpdateSaleItemRequest
{
    public int Id { get; set; }
    public int SaleId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}