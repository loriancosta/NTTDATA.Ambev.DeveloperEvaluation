namespace Ambev.DeveloperEvaluation.Application.Messaging.SaleEvents;

public class SaleCreatedEvent : IEvent
{
    public string EventName => "SaleCreated";
    public DateTime EventDate { get; } = DateTime.UtcNow;
    public int SaleId { get; set; }
    public int CustomerId { get; set; }
    public int BranchId { get; set; }
    public decimal TotalAmount { get; set; }
}

public class SaleUpdatedEvent : IEvent
{
    public string EventName => "SaleUpdated";
    public DateTime EventDate { get; } = DateTime.UtcNow;
    public int SaleId { get; set; }
    public decimal ModifiedAmount { get; set; }
}

public class SaleCancelledEvent : IEvent
{
    public string EventName => "SaleCancelled";
    public DateTime EventDate { get; } = DateTime.UtcNow;
    public int SaleId { get; set; }
}

public class ItemCancelledEvent : IEvent
{
    public string EventName => "ItemCancelled";
    public DateTime EventDate { get; } = DateTime.UtcNow;
    public int SaleId { get; set; }
    public int ItemId { get; set; }
}
