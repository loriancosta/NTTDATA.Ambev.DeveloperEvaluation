using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public int SaleId { get; set; }
        public Sale Sale { get; set; }


        public SaleItem(string productName, int quantity, decimal unitPrice)
        {
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            CalculateDiscount();
            CalculateTotalAmount();
        }

        public void CalculateDiscount()
        {
            if (Quantity >= 10 && Quantity <= 20)
                Discount = UnitPrice * 0.20m; // 20% discount
            else if (Quantity >= 4)
                Discount = UnitPrice * 0.10m; // 10% discount
            else
                Discount = 0m; // no discount
        }

        public void CalculateTotalAmount()
        {
            TotalAmount = (UnitPrice * Quantity) - Discount;
        }

        public void CancelItem()
        {
            TotalAmount = 0;
            Discount = 0;
        }
    }
}
