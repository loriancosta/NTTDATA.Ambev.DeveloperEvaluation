namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleResponse
    {
        public int Id { get; set; }
        public string SaleNumber { get; set; }
        public int CustomerId { get; set; }
        public int BranchId { get; set; }
        public decimal TotalAmount { get; set; }
    }

}
