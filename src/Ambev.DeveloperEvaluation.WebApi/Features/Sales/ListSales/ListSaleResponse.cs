namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale;

public class ListSaleResponse
{
    public List<SaleResponse> Items { get; set; }

}

public class SaleResponse
{ 
    public int Id { get; set; }
    public string SaleNumber { get; set; }
    public int CustomerId { get; set; }
    public int BranchId { get; set; }
    public decimal TotalAmount { get; set; }
    public List<SaleItemResponse> SaleItems { get; set; }
}

public class SaleItemResponse
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
}