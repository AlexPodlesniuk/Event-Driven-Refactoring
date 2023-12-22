using BuildingBlocks;

namespace eShoppo.Orders.Domain.Order;

public class OrderNotFoundException : AggregateNotFoundException
{
    public OrderNotFoundException(string id)
        : base(id, typeof(Order))
    {
    }
}