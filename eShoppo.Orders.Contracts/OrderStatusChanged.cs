using BuildingBlocks;

namespace eShoppo.Orders.Contracts;

public record OrderStatusChanged(string OrderId, string OrderStatus, DateTime SubmittedOn) : IDomainEvent;