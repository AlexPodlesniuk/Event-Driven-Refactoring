using BuildingBlocks;
using eShoppo.Orders.Contracts;

namespace eShoppo.Orders.Domain.Order;

public class Order(string id, string customerId, OrderStatus orderStatus, List<OrderItem> items) : AggregateRoot(id)
{
    public string CustomerId { get; } = customerId;
    public OrderStatus Status { get; private set; } = orderStatus;
    public IReadOnlyList<OrderItem> OrderItems => items.AsReadOnly();
    public int TotalItems => items.Sum(x => x.Quantity);
    public decimal TotalPrice => items.Sum(x => x.Price * x.Quantity);
    
    public static Order NewOrder(string customerId, List<OrderItem> orderItems) 
        => new(Guid.NewGuid().ToString(), customerId, OrderStatus.Undefined, orderItems);

    public void Create()
    {
        Status = OrderStatus.Created;
        RaiseEvent(new OrderCreated(Id, DateTime.UtcNow));
    }
    
    public void Submit()
    {
        Status = OrderStatus.Submitted;
        RaiseEvent(new OrderSubmitted(Id, DateTime.UtcNow));
    }
    
    public void AddItem(string productId, int quantity)
    {
        items.Add(new OrderItem(productId, quantity));
    }

    public void Cancel()
    {
        Status = OrderStatus.Cancelled;
        RaiseEvent(new OrderCancelled(Id, DateTime.UtcNow));
    }
    
    public void MarkAsPaid()
    {
        Status = OrderStatus.Paid;
        RaiseEvent(new OrderPaid(Id, DateTime.UtcNow));
    }
}