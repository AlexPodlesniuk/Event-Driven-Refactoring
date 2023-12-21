using System.Text.Json;
using BuildingBlocks;

namespace eShoppo.Orders.Domain;

public class Order(string id) : AggregateRoot(id)
{
    public bool IsSubmitted { get; private set; }

    public void Submit()
    {
        IsSubmitted = true;
        RaiseEvent(new OrderSubmitted(Id, DateTime.UtcNow));
    }

    public override string ToString() => JsonSerializer.Serialize(this);
}