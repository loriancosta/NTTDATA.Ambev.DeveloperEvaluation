namespace Ambev.DeveloperEvaluation.Application.Sale.ListSale;

public class ListSaleResult
{
    public List<SaleResult> Sales { get; set; }
}

public class SaleResult
{
    public int Id { get; set; }
    public string SaleNumber { get; set; }
    public int CustomerId { get; set; }
    public int BranchId { get; set; }
    public decimal TotalAmount { get; set; }
    public List<SaleItemResult> SaleItems { get; set; }
}

public class SaleItemResult
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
}