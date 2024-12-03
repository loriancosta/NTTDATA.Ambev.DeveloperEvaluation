using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale
{
    private string _saleNumber;
    private DateTime _saleDate;

    public Sale()
    {
        _saleNumber = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
        _saleDate = DateTime.UtcNow;
    }

    [Key]
    public int Id { get; set; }

    public string SaleNumber
    {
        get { return _saleNumber; }
        set { _saleNumber = value; }
    }

    public DateTime SaleDate
    {
        get { return _saleDate; }
        set { _saleDate = value.ToUniversalTime(); }
    }

    public int CustomerId { get; set; }
    public int BranchId { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
    public List<SaleItem> SaleItems { get; set; } = new List<SaleItem>();

    public void AddItem(SaleItem item)
    {
        SaleItems.Add(item);
        TotalAmount += item.TotalAmount;
    }

    public void CancelSale()
    {
        IsCancelled = true;
        foreach (var item in SaleItems)
        {
            item.CancelItem();
        }
    }
}
