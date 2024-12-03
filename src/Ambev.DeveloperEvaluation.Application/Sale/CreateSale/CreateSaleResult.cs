namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale;

public class CreateSaleResult
{
    public int Id { get; set; }
    public string SaleNumber { get; set; }
    public int CustomerId { get; set; }
    public int BranchId { get; set; }
    public decimal TotalAmount { get; set; }
    public List<CreateSaleItemResult> Items { get; set; }
}

public class CreateSaleItemResult
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
}