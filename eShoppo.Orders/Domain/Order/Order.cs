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
        ChangeStatus(OrderStatus.Created);
        RaiseEvent(new OrderCreated(Id, DateTime.UtcNow));
    }
    
    public void Submit()
    {
        ChangeStatus(OrderStatus.Submitted);
        RaiseEvent(new OrderSubmitted(Id, DateTime.UtcNow));
    }

    public void Cancel()
    {
        ChangeStatus(OrderStatus.Cancelled);
        RaiseEvent(new OrderCancelled(Id, DateTime.UtcNow));
    }
    
    public void MarkAsPaid()
    {
        ChangeStatus(OrderStatus.Paid);
        RaiseEvent(new OrderPaid(Id, DateTime.UtcNow));
    }
    
    private void ChangeStatus(OrderStatus status)
    {
        Status = status;
        RaiseEvent(new OrderStatusChanged(Id, status.ToString(), DateTime.UtcNow));
    }
}