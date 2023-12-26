using BuildingBlocks;

namespace eShoppo.Orders.Domain.Product;

public class OrderProduct : AggregateRoot
{
    public OrderProduct(string id, decimal price) : base(id)
    {
        Price = price;
    }
    
    public decimal Price { get; init; }
}