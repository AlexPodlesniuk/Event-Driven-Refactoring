using BuildingBlocks;

namespace eShoppo.Inventory.Domain;

public class StockRequestIsNotFoundException : AggregateNotFoundException
{
    public StockRequestIsNotFoundException(string id)
        : base(id, typeof(StockItemRequest))
    {
    }
}