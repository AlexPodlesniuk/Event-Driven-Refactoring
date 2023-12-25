using BuildingBlocks;

namespace eShoppo.Inventory.Domain;

public class StockItemRequest : AggregateRoot
{
    private readonly List<StockItem> _bookedStockItems = new();
    public StockItemRequest(string id) : base(id)
    {
        BookingStatus = PaymentRequestStatus.BookingCreated;
    }
    
   public IReadOnlyList<StockItem> BookedStockItems => _bookedStockItems.AsReadOnly();
    public PaymentRequestStatus BookingStatus { get; private set; }
    
    public void ConfirmBooking()
    {
        BookingStatus = PaymentRequestStatus.BookingConfirmed;
    }
    
    public void AddItem(string productId, int quantity)
    {
        _bookedStockItems.Add(new StockItem(productId, quantity));
    }
}