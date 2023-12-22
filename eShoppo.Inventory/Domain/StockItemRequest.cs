using BuildingBlocks;

namespace eShoppo.Inventory.Domain;

public class StockItemRequest : AggregateRoot
{
    private readonly List<StockItem> _bookedStockItems = new();
    public StockItemRequest(string id) : base(id)
    {
        BookingStatus = RequestStatus.BookingCreated;
    }
    
   public IReadOnlyList<StockItem> BookedStockItems => _bookedStockItems.AsReadOnly();
    public RequestStatus BookingStatus { get; private set; }
    
    public void ConfirmBooking()
    {
        BookingStatus = RequestStatus.BookingConfirmed;
        // make some real steps for inventory confirmation
    }
    
    public void AddItem(string productId, int quantity)
    {
        _bookedStockItems.Add(new StockItem(productId, quantity));
    }
}

public record StockItem(string ProductSku, int Quantity);