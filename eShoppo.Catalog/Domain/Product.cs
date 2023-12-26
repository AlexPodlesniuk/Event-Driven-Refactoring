using BuildingBlocks;
using eShoppo.Catalog.Contracts;

namespace eShoppo.Catalog.Domain;

public class Product(string id) : AggregateRoot(id)
{
    public string Name { get; init; }
    public string Sku { get; init; }
    public decimal Price { get; init; }
    public int MaxPaymentTime { get; init; }

    public void MarkAsInformationChanged()
    {
        RaiseEvent(new ProductInformationChanged(Id, Name, Sku, Price, MaxPaymentTime));
    }
}