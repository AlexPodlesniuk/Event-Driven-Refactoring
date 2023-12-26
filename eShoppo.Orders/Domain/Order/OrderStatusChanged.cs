using BuildingBlocks;

namespace eShoppo.Orders.Domain.Order;

public record OrderStatusChanged(string OrderId, string OrderStatus, DateTime SubmittedOn) : IDomainEvent;