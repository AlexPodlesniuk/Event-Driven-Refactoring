using BuildingBlocks;

namespace eShoppo.Orders.Domain.Order;

public record OrderCreated(string OrderId, DateTime CreatedOn) : IDomainEvent;