namespace Ambev.DeveloperEvaluation.Application.Sale.UpdateSale;

public class UpdateSaleResult
{
    public int Id { get; set; }
    public string SaleNumber { get; set; }
    public int CustomerId { get; set; }
    public int BranchId { get; set; }
    public decimal TotalAmount { get; set; }
    public List<UpdateSaleItemResult> Items { get; set; }
}

public class UpdateSaleItemResult
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
}