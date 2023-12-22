using BuildingBlocks;
using eShoppo.Orders.Contracts;

namespace eShoppo.Orders.Domain.Order;

public class Order(string id, string customerId, OrderStatus orderStatus) : AggregateRoot(id)
{
    public string CustomerId { get; } = customerId;
    private List<OrderLine> _orderLines = new();
    public OrderStatus Status { get; private set; } = orderStatus;
    public IReadOnlyList<OrderLine> OrderItems => _orderLines.AsReadOnly();
    public int TotalItems => _orderLines.Sum(x => x.Item.Quantity);
    public decimal TotalPrice => _orderLines.Sum(x => x.Price * x.Item.Quantity);
    
    public static Order NewOrder(string customerId) 
        => new(Guid.NewGuid().ToString(), customerId, OrderStatus.Undefined);

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
    
    public void AddOrderLine(OrderItem orderItem, decimal price) =>  _orderLines.Add(new OrderLine(orderItem, price));
    
    private void ChangeStatus(OrderStatus status)
    {
        Status = status;
        RaiseEvent(new OrderStatusChanged(Id, status.ToString(), DateTime.UtcNow));
    }
}