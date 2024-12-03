namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleResponse
    {
        public int Id { get; set; }
        public string SaleNumber { get; set; }
        public int CustomerId { get; set; }
        public int BranchId { get; set; }
        //public List<CreateSaleItemRequest> Items { get; set; }
        public decimal TotalAmount { get; set; }
    }

}
