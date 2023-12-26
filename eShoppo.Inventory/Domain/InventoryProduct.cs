using BuildingBlocks;

namespace eShoppo.Inventory.Domain;

public class InventoryProduct : AggregateRoot
{
    public InventoryProduct(string id, string sku) : base(id)
    {
        Sku = sku;
    }
    
    public string Sku { get; init; }
}